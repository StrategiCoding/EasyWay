using EasyWay.Internals;
using EasyWay.Internals.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyWay
{
    public static class Extensions
    {
        public static IServiceCollection AddFluentValidation(
            this IServiceCollection services,
            IEnumerable<Assembly> assemblies)
        {
            services.AddValidatorsFromAssemblies(assemblies);

            services.AddScoped(typeof(IEasyWayValidator<>), typeof(FluentValidator<>));

            return services;
        }
}
}
