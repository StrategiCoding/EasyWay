using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Queries
{
    internal static class Extensions
    {
        internal static IServiceCollection AddQueries(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddScoped<IQueryExecutor, QueryExecutor>();

            services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            return services;
        }
    }
}
