using EasyWay.Internals.Contexts;
using EasyWay.Internals.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Queries
{
    internal sealed class QueryExecutor<TModule> : IQueryExecutor<TModule>
        where TModule : EasyWayModule
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly ICancellationContextConstructor _cancellationContextConstructor;

        public QueryExecutor(
            IServiceProvider serviceProvider,
            ICancellationContextConstructor cancellationContextConstructor) 
        { 
            _serviceProvider = serviceProvider;
            _cancellationContextConstructor = cancellationContextConstructor;
        }  

        public async Task<QueryResult<TReadModel>> Execute<TQuery, TReadModel>(TQuery query, CancellationToken cancellationToken = default)
            where TQuery : Query<TReadModel>
            where TReadModel : ReadModel
        {
            _cancellationContextConstructor.Set(cancellationToken);

            var queryType = query.GetType();

            var validatorType = typeof(IEasyWayValidator<>).MakeGenericType(queryType);

            var validator = _serviceProvider.GetService<IEasyWayValidator<TQuery>>();

            if (validator is not null)
            {
                var errors = validator.Validate(query);

                if (errors.Any())
                {
                    return QueryResult<TReadModel>.Validation(errors);
                }
            }

            var queryHandler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TReadModel>>();

            var queryResult = await queryHandler.Handle(query);

            return queryResult;
        }
    }
}
