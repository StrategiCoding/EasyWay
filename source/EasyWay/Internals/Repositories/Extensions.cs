using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyWay.Internals.Repositories
{
    internal static class Extensions
    {
        internal static IServiceCollection AddRepositories(this IServiceCollection services, IEnumerable<Assembly> assemblies) 
        {
            services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo<IRepository>())
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            return services;
        }
    }
}
