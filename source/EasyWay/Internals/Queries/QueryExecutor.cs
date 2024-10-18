using EasyWay.Internals.Contexts;
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

        public Task<TReadModel> Execute<TQuery, TReadModel>(TQuery query, CancellationToken cancellationToken)
            where TQuery : Query<TModule, TReadModel>
            where TReadModel : ReadModel
        {
            _cancellationContextConstructor.Set(cancellationToken);

            return _serviceProvider
                    .GetRequiredService<IQueryHandler<TModule, TQuery, TReadModel>>()
                    .Handle(query);
        }
    }
}
