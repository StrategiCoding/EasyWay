using EasyWay.Internals.Commands;
using EasyWay.Internals.Contexts;
using EasyWay.Internals.DomainEvents;
using EasyWay.Internals.Queries;
using EasyWay.Internals.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyWay
{
    public static class Extensions
    {
        public static void AddEasyWay(this IServiceCollection services, params Assembly[] assemblies)
        {
            services
                .AddContexts()
                .AddCommands(assemblies)
                .AddQueries(assemblies)
                .AddDomainEvents(assemblies);

            services.AddUnitOfWorkCommandHandlerDecorator();  
        }

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
