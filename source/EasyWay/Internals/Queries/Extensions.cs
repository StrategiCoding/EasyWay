using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Queries
{
    internal static class Extensions
    {
        internal static IServiceCollection AddQueries<TModule>(
            this IServiceCollection services,
            IEnumerable<Assembly> assemblies)
            where TModule : EasyWayModule
        {
            services.AddSingleton<IQueryExecutor<TModule>, QueryExecutor<TModule>>();

            services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            return services;
        }
    }
}
