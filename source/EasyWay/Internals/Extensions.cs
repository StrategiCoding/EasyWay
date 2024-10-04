using EasyWay.Internals.Commands;
using EasyWay.Internals.Contexts;
using EasyWay.Internals.DomainEvents;
using EasyWay.Internals.Queries;
using EasyWay.Internals.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyWay.Internals
{
    internal static class Extensions
    {
        internal static void AddEasyWay(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            services
                .AddContexts()
                .AddCommands(assemblies)
                .AddQueries(assemblies)
                .AddDomainEvents(assemblies)
                .AddRepositories(assemblies);

            services.AddUnitOfWorkCommandHandlerDecorator();
        }
    }
}
