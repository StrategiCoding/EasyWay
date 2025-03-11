using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Clocks
{
    internal static class Extensions
    {
        internal static IServiceCollection AddClocks(this IServiceCollection services)
        {
            InternalClock.Initialize();

            var clock = new Clock();

            services.AddSingleton(clock);
            services.AddSingleton(clock.TimeProvider);

            return services;
        }
    }
}
