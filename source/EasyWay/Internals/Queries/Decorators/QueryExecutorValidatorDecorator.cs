using EasyWay.Internals.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Queries.Decorators
{
    internal sealed class QueryExecutorValidatorDecorator : IQueryExecutor
    {
        private readonly IQueryExecutor _decoratedQueryExecutor;

        private readonly IServiceProvider _serviceProvider;

        public QueryExecutorValidatorDecorator(
            IQueryExecutor decoratedQueryExecutor,
            IServiceProvider serviceProvider)
        {
            _decoratedQueryExecutor = decoratedQueryExecutor;
            _serviceProvider = serviceProvider;
        }

        public Task<QueryResult<TReadModel>> Execute<TModule, TQuery, TReadModel>(TQuery query, CancellationToken cancellationToken = default)
            where TModule : EasyWayModule
            where TQuery : Query<TReadModel>
            where TReadModel : ReadModel
        {
            var validatorType = typeof(IEasyWayValidator<>).MakeGenericType(query.GetType());

            var validator = _serviceProvider.GetService<IEasyWayValidator<TQuery>>();

            if (validator is not null)
            {
                var errors = validator.Validate(query);

                if (errors.Any())
                {
                    return Task.FromResult(QueryResult<TReadModel>.Validation(errors));
                }
            }

            return _decoratedQueryExecutor.Execute<TModule, TQuery, TReadModel>(query, cancellationToken);
        }
    }
}
