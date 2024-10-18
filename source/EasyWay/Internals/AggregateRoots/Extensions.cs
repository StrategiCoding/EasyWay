using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.AggregateRoots
{
    internal static class Extensions
    {
        internal static IServiceCollection AddAggregateRoots(
            this IServiceCollection services)
        {
            services.AddScoped<IConcurrencyTokenUpdater, ConcurrencyTokenUpdater>();

            return services;
        }
    }
}
