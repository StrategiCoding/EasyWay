using EasyWay.Internals.Contexts;
using FluentValidation;
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

        public async Task<QueryResult<TReadModel>> Execute<TQuery, TReadModel>(TQuery query, CancellationToken cancellationToken)
            where TQuery : Query<TModule, TReadModel>
            where TReadModel : ReadModel
        {
            _cancellationContextConstructor.Set(cancellationToken);

            var validator = _serviceProvider.GetService<IValidator<TQuery>>();

            if (validator is not null)
            {
                var result = validator.Validate(query);

                if (!result.IsValid)
                {
                    return QueryResult<TReadModel>.Validation(result.ToDictionary());
                }
            }

            return await _serviceProvider
                    .GetRequiredService<IQueryHandler<TModule, TQuery, TReadModel>>()
                    .Handle(query);
        }
    }
}
