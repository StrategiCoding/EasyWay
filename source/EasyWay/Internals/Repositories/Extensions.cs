using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyWay.Internals.Repositories
{
    internal static class Extensions
    {
        private static Type _repositoryType = typeof(IRepository);

        internal static IServiceCollection AddRepositories(this IServiceCollection services, IEnumerable<Assembly> assemblies) 
        {
            services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.Where(x => x.IsAssignableTo(_repositoryType)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            return services;
        }
    }
}
