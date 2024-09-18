using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyWay.Internals.Events
{
    public static class ExtensionAddEvents
    {
        public static IServiceCollection AddEventHandlers(this IServiceCollection services, params Assembly[] assemblies) 
        {
            services.AddScoped<IEventPublisher, EventPublisher>();

            services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IEventHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            return services;
        }
    }
}
