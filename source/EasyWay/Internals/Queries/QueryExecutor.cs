using EasyWay.Internals.Contexts;
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

        public async Task<TReadModel> Execute<TQuery, TReadModel>(TQuery query, CancellationToken cancellationToken)
            where TQuery : Query<TReadModel>
            where TReadModel : ReadModel
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;

                sp
                .GetRequiredService<CancellationContext>()
                .Set(cancellationToken);

                return await sp
                .GetRequiredService<IQueryHandler<TQuery, TReadModel>>()
                .Handle(query)
                .ConfigureAwait(false);
            }
        }
    }
}
