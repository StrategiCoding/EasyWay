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

        public async Task<QueryResult<TReadModel>> Execute<TModule, TQuery, TReadModel>(TQuery query, CancellationToken cancellationToken = default)
            where TModule : EasyWayModule
            where TQuery : Query<TReadModel>
            where TReadModel : ReadModel
        {
            return await _serviceProvider.GetRequiredService<QueryHandler<TQuery, TReadModel>>().Handle(query);
        }
    }
}
