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

        public async Task<QueryResult<TReadModel>> Execute<TModule, TQuery, TReadModel>(TQuery query, CancellationToken cancellationToken = default)
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
                    return QueryResult<TReadModel>.Validation(errors);
                }
            }

            QueryResult<TReadModel> result;

            try
            {
                result = await _serviceProvider.GetRequiredService<QueryHandler<TQuery, TReadModel>>().Handle(query);
            }
            catch (OperationCanceledException)
            {
                return QueryResult<TReadModel>.OperationCanceled();
            }
            catch (Exception exception)
            {
                return QueryResult<TReadModel>.UnknownException(exception);
            }

            return result;
        }
    }
}
