using EasyWay.Internals.CancellationTokens;
using EasyWay.Internals.Events;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyWay
{
    public static class ExtensionAddEasyWay
    {
        public static void AddEasyWay(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddCancellationToken();

            services
                .AddCommandHandlers(assemblies)
                .AddQueryHandlers(assemblies)
                .AddEventHandlers(assemblies);
        }

        private static IServiceCollection AddCommandHandlers(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            return services;
        }

        private static IServiceCollection AddQueryHandlers(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            return services;
        }
    }
}
