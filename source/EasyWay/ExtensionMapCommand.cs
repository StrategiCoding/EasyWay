using EasyWay.Internals.Commands;
using EasyWay.Internals.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace EasyWay
{
    public static class ExtensionMapCommand
    {
        public static RouteHandlerBuilder MapCommand<TCommand>(this IEndpointRouteBuilder endpoints)
            where TCommand : Command
        {
            return endpoints.MapPost(typeof(TCommand).Name, async ([FromBody] TCommand command, ICommandExecutor executor, CancellationToken cancellationToken) =>
            {
                await executor.Execute(command, cancellationToken).ConfigureAwait(false);
            });
        }
    }
}
