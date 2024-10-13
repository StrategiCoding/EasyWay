using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyWay.Internals.Policies
{
    internal static class Extensions
    {
        internal static IServiceCollection AddPolicies(
            this IServiceCollection services,
            IEnumerable<Assembly> assemblies) 
        {
            services.Scan(s => s.FromAssemblies(assemblies)
                .AddClasses(c => c.AssignableTo(typeof(IPolicy<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            services.Scan(s => s.FromAssemblies(assemblies)
                .AddClasses(c => c.AssignableTo(typeof(IPolicy<,,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            return services;
        }
    }
}
