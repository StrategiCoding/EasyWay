using EasyWay.Internals.Commands;
using EasyWay.Internals.Exceptions;
using EasyWay.Internals.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace EasyWay
{
    public static class Extensions
    {
        public static void UseEasyWay(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }

        public static RouteHandlerBuilder MapQuery<TQuery, TReadModel>(this IEndpointRouteBuilder endpoints)
            where TQuery : Query<TReadModel>
            where TReadModel : ReadModel
        {
            return endpoints.MapPost(typeof(TQuery).Name, async ([FromBody] TQuery query, IQueryExecutor executor, CancellationToken cancellationToken) =>
            {
                await executor.Execute<TQuery, TReadModel>(query, cancellationToken).ConfigureAwait(false);
            });
        }

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
