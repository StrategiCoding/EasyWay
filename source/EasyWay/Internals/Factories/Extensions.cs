using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyWay.Internals.Factories
{
    internal static class Extensions
    {
        private static string _postfix = "Factory";

        internal static IServiceCollection AddFactories(this IServiceCollection services, IEnumerable<Assembly> assemblies) 
        {
            services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.Where(x => x.Name.EndsWith(_postfix)))
            .AsSelf()
            .WithTransientLifetime());

            return services;
        }
    }
}
