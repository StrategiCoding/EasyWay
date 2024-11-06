using EasyWay.Internals.Commands;
using EasyWay.Internals.Queries;
using EasyWay.Internals.Queries.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace EasyWay
{
    public static class Extensions
    {
        public static IEndpointConventionBuilder MapQuery<TModule, TQuery, TReadModel>(this IEndpointRouteBuilder endpoints)
            where TModule : EasyWayModule
            where TQuery : Query<TModule, TReadModel>
            where TReadModel : ReadModel
        {
            return endpoints.MapPost(typeof(TModule).Name + "/_queries/" + typeof(TQuery).Name, async ([FromBody] TQuery query, IQueryExecutor<TModule> executor, CancellationToken cancellationToken) =>
            {
                var queryResult = await executor.Execute<TQuery, TReadModel>(query, cancellationToken);

                return queryResult.Error switch
                {
                    QueryErrorEnum.None => Results.Ok(queryResult.ReadModel),
                    QueryErrorEnum.NotFound => Results.StatusCode(404),
                    QueryErrorEnum.Forbidden => Results.StatusCode(403),
                    _ => Results.StatusCode(500),
                };
            });
        }

        public static IEndpointConventionBuilder MapCommand<TModule, TCommand>(this IEndpointRouteBuilder endpoints)
            where TModule : EasyWayModule
            where TCommand : Command<TModule>
        {
            return endpoints.MapPost(typeof(TModule).Name + "/_commands/" + typeof(TCommand).Name, async ([FromBody] TCommand command, ICommandExecutor<TModule> executor, CancellationToken cancellationToken) =>
            {
                await executor.Execute(command, cancellationToken);
            });
        }

        public static IEndpointConventionBuilder MapCommand<TModule, TCommand, TCommandResult>(this IEndpointRouteBuilder endpoints)
            where TModule : EasyWayModule
            where TCommand : Command<TModule, TCommandResult>
            where TCommandResult : CommandResult
        {
            return endpoints.MapPost(typeof(TModule).Name + "/_commands/" + typeof(TCommand).Name, async ([FromBody] TCommand command, ICommandExecutor<TModule> executor, CancellationToken cancellationToken) =>
            {
                return await executor.Execute<TCommand, TCommandResult>(command, cancellationToken);
            });
        }
    }
}
