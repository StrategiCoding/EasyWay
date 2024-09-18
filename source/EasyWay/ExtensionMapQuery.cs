using EasyWay.Internals.CancellationTokens;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay
{
    public static class ExtensionMapQuery
    {
        public static RouteHandlerBuilder MapQuery<TQuery, TResult>(this IEndpointRouteBuilder endpoints)
            where TQuery : Query<TResult>
        {
            return endpoints.MapPost(typeof(TQuery).Name, async ([FromBody] TQuery query, IServiceProvider serviceProvider, CancellationToken cancellationToken) =>
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var sp = scope.ServiceProvider;

                    sp
                    .GetRequiredService<CancellationTokenProvider>()
                    .Set(cancellationToken);

                    return await sp
                    .GetRequiredService<IQueryHandler<TQuery, TResult>>()
                    .Handle(query)
                    .ConfigureAwait(false);
                }
            });
        }
    }
}
