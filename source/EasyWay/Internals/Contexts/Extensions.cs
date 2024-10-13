using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Contexts
{
    internal static class Extensions
    {
        internal static IServiceCollection AddContexts(this IServiceCollection services)
        {
            services.AddScoped<CancellationContext>();
            services.AddScoped<ICancellationContext, CancellationContext>();

            services.AddScoped<IUserContext, DefaultUserContext>();
            services.AddScoped<ICorrelationContext, DefaultCorrelationContext>();

            return services;
        }
    }
}
