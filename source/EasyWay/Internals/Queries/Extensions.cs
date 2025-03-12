using System.Reflection;
using EasyWay.Internals.Queries.Decorators;
using EasyWay.Internals.Validation;
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

                var cancellationContext = new QueryExecutorCancellationContextDecorator(executor, p);

                var validator = new QueryExecutorValidatorDecorator(cancellationContext, p);

                var logger = new QueryExecutorLoggerDecorator(validator, p);

                return logger;
            });

            services.AddAsBasedType(typeof(QueryHandler<,>), ServiceLifetime.Scoped, assemblies);

            return services;
        }
    }
}
