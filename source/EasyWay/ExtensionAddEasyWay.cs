using EasyWay.Internals.CancellationTokens;
using EasyWay.Internals.Commands;
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
                .AddCancellationToken()
                .AddCommandHandlers(assemblies)
                .AddQueryHandlers(assemblies)
                .AddDomainEventHandlers(assemblies);

            services.AddUnitOfWorkCommandHandlerDecorator();  
        }
    }
}
