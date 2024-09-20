using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Commands
{
    internal static class ExtensionAddCommandHandlers
    {
        internal static IServiceCollection AddCommandHandlers(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            return services;
        }
    }
}
