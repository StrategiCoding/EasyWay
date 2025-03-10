using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyWay.Internals.Factories
{
    internal static class Extensions
    {
        internal static IServiceCollection AddFactories(
            this IServiceCollection services,
            IEnumerable<Assembly> assemblies)
        {
            services.AddSelfOnBasedType(typeof(Factory), ServiceLifetime.Transient, assemblies);

            return services;
        }
    }
}
