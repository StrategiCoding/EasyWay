using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyWay.Internals.DomainServices
{
    internal static class Extensions
    {
        private static string _postfix = "DomainService";

        internal static IServiceCollection AddDomainServices(
            this IServiceCollection services,
            IEnumerable<Assembly> assemblies) 
        {
            services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.Where(x => x.Name.EndsWith(_postfix)))
            .AsSelf()
            .WithTransientLifetime());

            return services;
        }
    }
}
