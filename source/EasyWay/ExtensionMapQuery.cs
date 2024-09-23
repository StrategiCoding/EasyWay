using EasyWay.Internals.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay
{
    public static class ExtensionMapQuery
    {
        public static RouteHandlerBuilder MapQuery<TQuery, TReadModel>(this IEndpointRouteBuilder endpoints)
            where TQuery : Query<TReadModel>
            where TReadModel : ReadModel
        {
            return endpoints.MapPost(typeof(TQuery).Name, async ([FromBody] TQuery query, IServiceProvider serviceProvider, CancellationToken cancellationToken) =>
            {
                using (var scope = serviceProvider.CreateScope())
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
            });
        }
    }
}
