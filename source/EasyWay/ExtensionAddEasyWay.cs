using EasyWay.Internals.BusinessRules;
using EasyWay.Internals.Commands;
using EasyWay.Internals.Contexts;
using EasyWay.Internals.DomainEvents;
using EasyWay.Internals.Queries;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyWay
{
    public static class ExtensionAddEasyWay
    {
        public static void AddEasyWay(this IServiceCollection services, params Assembly[] assemblies)
        {
            services
                .AddContexts()
                .AddCommands(assemblies)
                .AddQueries(assemblies)
                .AddDomainEventHandlers(assemblies)
                .AddBrokenBusinessRuleHandlers(assemblies);

            services.AddUnitOfWorkCommandHandlerDecorator();  
        }
    }
}
