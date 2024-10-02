using EasyWay.Internals.Commands;
using EasyWay.Internals.Contexts;
using EasyWay.Internals.DomainEvents;
using EasyWay.Internals.Queries;
using EasyWay.Internals.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyWay
{
    public static class Extensions
    {
        public static void AddEasyWay(this IServiceCollection services, IEnumerable<Assembly> assemblies)
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
