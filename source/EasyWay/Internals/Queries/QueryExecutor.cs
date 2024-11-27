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

        public async Task<QueryResult<TReadModel>> Execute<TReadModel>(Query<TModule, TReadModel> query, CancellationToken cancellationToken)
            where TReadModel : ReadModel
        {
            _cancellationContextConstructor.Set(cancellationToken);

            var queryType = query.GetType();

            var validatorType = typeof(IEasyWayValidator<>).MakeGenericType(queryType);

            var validator = _serviceProvider.GetService(validatorType);

            if (validator is not null)
            {
                var errors = (IDictionary<string, string[]>)validatorType
                    .GetMethod("Validate")
                    ?.Invoke(validator, [query]);

                if (errors.Any())
                {
                    return QueryResult<TReadModel>.Validation(errors);
                }
            }

            var queryHandlerType = typeof(IQueryHandler<,,>).MakeGenericType(typeof(TModule), queryType, typeof(TReadModel));

            var queryHandler = _serviceProvider.GetRequiredService(queryHandlerType);

            var queryResult = await (Task<QueryResult<TReadModel>>) queryHandlerType.GetMethod("Handle").Invoke(queryHandler, [query]);

            return queryResult;
        }
    }
}
