using EasyWay.Internals.AccessTokenCreators;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals
{
    internal static class Extensions
    {
        internal static IServiceCollection AddAuth(this IServiceCollection services)
        {
            services.AddSingleton<IAccessTokensCreator, AccessTokensCreator>();

            return services;
        }
    }
}
