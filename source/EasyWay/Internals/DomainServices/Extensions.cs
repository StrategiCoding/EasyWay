using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyWay.Internals.DomainServices
{
    internal static class Extensions
    {
        private static Type _domainServiceType = typeof(DomainService);

        internal static IServiceCollection AddDomainServices(
            this IServiceCollection services,
            IEnumerable<Assembly> assemblies) 
        {
            services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.Where(x => x.IsSubclassOf(_domainServiceType)))
            .AsSelf()
            .WithTransientLifetime());

            return services;
        }
    }
}
