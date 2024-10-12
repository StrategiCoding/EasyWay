using EasyWay.Internals.Commands;
using EasyWay.Internals.Contexts;
using EasyWay.Internals.DomainEvents;
using EasyWay.Internals.DomainServices;
using EasyWay.Internals.Factories;
using EasyWay.Internals.Queries;
using EasyWay.Internals.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyWay.Internals
{
    internal static class Extensions
    {
        internal static void AddEasyWay<TModule>(this IServiceCollection services, IEnumerable<Assembly> assemblies)
            where TModule : EasyWayModule
        {
            services
                .AddContexts()
                .AddCommands(assemblies)
                .AddQueries<TModule>(assemblies)
                .AddDomainEvents(assemblies)
                .AddRepositories(assemblies)
                .AddDomainServices(assemblies)
                .AddFactories(assemblies);

            services.AddUnitOfWorkCommandHandlerDecorator();
        }
    }
}
