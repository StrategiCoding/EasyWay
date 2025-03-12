using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Queries.Decorators
{
    internal sealed class QueryExecutorCancellationContextDecorator : IQueryExecutor
    {
        private readonly IQueryExecutor _decoratedQueryExecutor;

        private readonly IServiceProvider _serviceProvider;

        public QueryExecutorCancellationContextDecorator(
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
            _serviceProvider.GetRequiredService<CancellationContext>().Set(cancellationToken);

            return _decoratedQueryExecutor.Execute<TModule, TQuery, TReadModel>(query, cancellationToken);
        }
    }
}
