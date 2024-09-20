using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Queries
{
    internal static class ExtensionAddQueryHandlers
    {
        internal static IServiceCollection AddQueryHandlers(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            return services;
        }
    }
}
