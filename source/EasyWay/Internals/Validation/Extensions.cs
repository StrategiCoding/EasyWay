using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyWay.Internals.Validation
{
    internal static class Extensions
    {
        internal static IServiceCollection AddValidation(
            this IServiceCollection services,
            IEnumerable<Assembly> assemblies)
        {
            services.AddValidatorsFromAssemblies(assemblies);

            return services;
        }
    }
}
