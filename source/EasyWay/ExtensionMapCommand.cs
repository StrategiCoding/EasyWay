using EasyWay.Internals.CancellationTokens;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay
{
    public static class ExtensionMapCommand
    {
        public static RouteHandlerBuilder MapCommand<TCommand>(this IEndpointRouteBuilder endpoints)
            where TCommand : class, ICommand
        {
            var url = typeof(TCommand).Name;

            return endpoints.MapPost(url, async ([FromBody] TCommand command, IServiceProvider serviceProvider, CancellationToken cancellationToken) =>
            {
                using (var scope = serviceProvider.CreateScope())
                {
                    var sp = scope.ServiceProvider;

                    sp
                    .GetRequiredService<CancellationTokenProvider>()
                    .Set(cancellationToken);

                    await sp
                    .GetRequiredService<ICommandHandler<TCommand>>().Handle(command)
                    .ConfigureAwait(false);
                }
            });
        }
    }
}
