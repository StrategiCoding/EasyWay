using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.CancellationTokens
{
    internal static class ExtensionAddCancellationToken
    {
        internal static IServiceCollection AddCancellationToken(this IServiceCollection services) 
        {
            services.AddScoped<CancellationTokenProvider>();
            services.AddScoped<ICancellationTokenProvider, CancellationTokenProvider>();

            return services;
        }
    }
}
