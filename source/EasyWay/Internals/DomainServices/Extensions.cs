using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyWay.Internals.DomainServices
{
    internal static class Extensions
    {
        internal static IServiceCollection AddDomainServices(
            this IServiceCollection services,
            IEnumerable<Assembly> assemblies) 
        {
            services.AddSelfOnBasedType(typeof(DomainService), ServiceLifetime.Transient, assemblies);

            return services;
        }
    }
}
