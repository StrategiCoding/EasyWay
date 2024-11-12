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
            services.Scan(s => s.FromAssemblies(assemblies)
                .AddClasses(c => c.AssignableTo(typeof(IInitializer)))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            return services;
        }
}
}
