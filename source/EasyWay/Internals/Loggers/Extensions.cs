using EasyWay.Internals.Queries.Loggers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EasyWay.Internals.Loggers
{
    internal static class Extensions
    {
        internal static IServiceCollection AddLoggers(this IServiceCollection services, Type moduleType)
        {
            services.AddLogging(x => x.AddJsonConsole());

            var loggerType = typeof(EasyWayLogger<>).MakeGenericType(moduleType);

            services.AddSingleton(loggerType);

            return services;
        }
    }
}
