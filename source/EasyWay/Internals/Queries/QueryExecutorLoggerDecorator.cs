using EasyWay.Internals.Queries.Loggers;
using EasyWay.Internals.Queries.Results;
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

            var userContext = _serviceProvider.GetRequiredService<IUserContext>();

            //TODO begin scope (correlation Id, userId)

            if (userContext.UserId is not null)
            {
                logger.ExecutingByUser(query, userContext.UserId);
            }
            else
            {
                logger.Executing(query);
            }

            try
            {
                var result = await _decoratedQueryExecutor.Execute<TModule, TQuery, TReadModel>(query, cancellationToken);

                switch (result.Error)
                {
                    case QueryErrorEnum.None: logger.Successed(result.ReadModel); break;
                    case QueryErrorEnum.Validation: logger.Validation(result.ValidationErrors); break;
                    case QueryErrorEnum.OperationCanceled: logger.OperationCanceled(); break;
                    case QueryErrorEnum.NotFound: logger.NotFound(); break;
                    case QueryErrorEnum.Forbidden: logger.Forbidden(); break;
                    default: logger.UnexpectedException(result.Exception); break;
                }

                return result;
            }
            catch (Exception ex)
            {
                logger.UnexpectedException(ex);

                return QueryResult<TReadModel>.UnknownException(ex);
            }
        }
    }
}
