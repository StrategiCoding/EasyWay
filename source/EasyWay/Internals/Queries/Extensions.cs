using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Queries
{
    internal static class Extensions
    {
        internal static IServiceCollection AddQueries(
            this IServiceCollection services,
            Type moduleType,
            IEnumerable<Assembly> assemblies)
        {
            services.AddScoped(typeof(IQueryExecutor<>).MakeGenericType(moduleType), typeof(QueryExecutor<>).MakeGenericType(moduleType));

            services.AddAsBasedType(typeof(QueryHandler<,>), ServiceLifetime.Scoped, assemblies);

            return services;
        }



    }
}
