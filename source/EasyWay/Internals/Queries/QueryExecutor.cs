using EasyWay.Internals.Contexts;
using FluentValidation;
using FluentValidation.Results;
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

            var validatorType = typeof(IValidator<>).MakeGenericType(queryType);

            var validator = _serviceProvider.GetService(validatorType);

            if (validator is not null)
            {
                var result = (ValidationResult)validatorType
                    .GetMethod("Validate")
                    ?.Invoke(validator, [query]);

                if (!result.IsValid)
                {
                    var errors = result.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(x => x.ErrorCode).ToArray()
                    );

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
