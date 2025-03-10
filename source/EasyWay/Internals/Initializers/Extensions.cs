using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyWay.Internals.Initializers
{
    internal static class Extensions
    {
        internal static IServiceCollection AddInitializers(
            this IServiceCollection services,
            IEnumerable<Assembly> assemblies) 
        {
            services.AddAsImplementedInterfaces(typeof(IInitializer), ServiceLifetime.Transient, assemblies);

            return services;
        }
}
}
