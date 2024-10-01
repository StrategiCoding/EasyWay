using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyWay.Internals.Repositories
{
    internal static class Extensions
    {
        private static string _postfix = "Repository";

        internal static IServiceCollection AddRepositories(this IServiceCollection services, params Assembly[] assemblies) 
        {
            services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.Where(x => x.Name.EndsWith(_postfix)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            return services;
        }
    }
}
