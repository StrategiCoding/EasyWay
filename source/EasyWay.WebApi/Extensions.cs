using EasyWay.Internals.Commands;
using EasyWay.Internals.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace EasyWay
{
    public static class Extensions
    {
        public static RouteHandlerBuilder MapQuery<TModule, TQuery, TReadModel>(this IEndpointRouteBuilder endpoints)
            where TModule : EasyWayModule
            where TQuery : Query<TModule, TReadModel>
            where TReadModel : ReadModel
        {
            return endpoints.MapPost(typeof(TModule).Name + '/' + typeof(TQuery).Name, async ([FromBody] TQuery query, IQueryExecutor<TModule> executor, CancellationToken cancellationToken) =>
            {
                await executor.Execute<TQuery, TReadModel>(query, cancellationToken).ConfigureAwait(false);
            });
        }

        public static RouteHandlerBuilder MapCommand<TModule, TCommand>(this IEndpointRouteBuilder endpoints)
            where TModule : EasyWayModule
            where TCommand : Command<TModule>
        {
            return endpoints.MapPost(typeof(TModule).Name + '/' + typeof(TCommand).Name, async ([FromBody] TCommand command, ICommandExecutor<TModule> executor, CancellationToken cancellationToken) =>
            {
                await executor.Execute(command, cancellationToken).ConfigureAwait(false);
            });
        }
    }
}
