using EasyWay.Internals.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Queries
{
    internal sealed class QueryExecutor : IQueryExecutor
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryExecutor(IServiceProvider serviceProvider) 
        { 
            _serviceProvider = serviceProvider;
        }  

        public Task<QueryResult<TReadModel>> Execute<TModule, TQuery, TReadModel>(TQuery query, CancellationToken cancellationToken = default)
            where TModule : EasyWayModule
            where TQuery : Query<TReadModel>
            where TReadModel : ReadModel
        {
            _serviceProvider.GetRequiredService<CancellationContext>().Set(cancellationToken);

            var validator = _serviceProvider.GetService<IEasyWayValidator<TQuery>>();

            if (validator is not null)
            {
                var errors = validator.Validate(query);

                if (errors.Any())
                {
                    return Task.FromResult(QueryResult<TReadModel>.Validation(errors));
                }
            }

            return _serviceProvider.GetRequiredService<QueryHandler<TQuery, TReadModel>>().Handle(query);
        }
    }
}
