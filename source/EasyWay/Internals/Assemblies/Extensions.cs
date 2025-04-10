using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyWay.Internals.Assemblies
{
    internal static class Extensions
    {
        internal static IServiceCollection AddAssemblies(
            this IServiceCollection services,
            IEnumerable<Assembly> assemblies)
        {
            var types = assemblies.SelectMany(x => x.GetTypes()).Distinct();

            new EntitiesFieldsInfo(types);

            return services;
        }
    }
}
