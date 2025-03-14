using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Queries
{
    internal static class Extensions
    {
        internal static IServiceCollection AddQueries(
            this IServiceCollection services,
            IEnumerable<Assembly> assemblies)
        {
            services.AddScoped<QueryExecutor>();

            services.AddScoped<IQueryExecutor>(p =>
            {
                var executor = p.GetRequiredService<QueryExecutor>();

                return new QueryExecutorLoggerDecorator(executor, p);
            });

            services.AddAsBasedType(typeof(QueryHandler<,>), ServiceLifetime.Scoped, assemblies);

            return services;
        }
    }
}
