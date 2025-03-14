using EasyWay.Internals.Queries.Loggers;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Queries
{
    internal sealed class QueryExecutorLoggerDecorator : IQueryExecutor
    {
        private readonly IQueryExecutor _decoratedQueryExecutor;

        private readonly IServiceProvider _serviceProvider;

        public QueryExecutorLoggerDecorator(
            IQueryExecutor decoratedQueryExecutor,
            IServiceProvider serviceProvider)
        {
            _decoratedQueryExecutor = decoratedQueryExecutor;
            _serviceProvider = serviceProvider;
        }

        public async Task<QueryResult<TReadModel>> Execute<TModule, TQuery, TReadModel>(TQuery query, CancellationToken cancellationToken = default)
            where TModule : EasyWayModule
            where TQuery : Query<TReadModel>
            where TReadModel : ReadModel
        {
            var logger = _serviceProvider.GetRequiredService<EasyWayLogger<TModule>>();

            //TODO begin scope (correlation Id)

            logger.Executing(query);

            try
            {
                var result = await _decoratedQueryExecutor.Execute<TModule, TQuery, TReadModel>(query, cancellationToken);

                logger.Executed();

                return result;
            }
            catch (Exception ex)
            {
                logger.UnexpectedException(ex);
                throw;
            } 
        }
    }
}
