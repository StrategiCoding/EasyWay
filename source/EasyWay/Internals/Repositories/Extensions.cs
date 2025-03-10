using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyWay.Internals.Repositories
{
    internal static class Extensions
    {
        private static Type _repositoryType = typeof(IRepository);

        internal static IServiceCollection AddRepositories(this IServiceCollection services, IEnumerable<Assembly> assemblies) 
        {
            services.AddAsImplementedInterfaces(typeof(IRepository), ServiceLifetime.Scoped, assemblies);

            return services;
        }
    }
}
