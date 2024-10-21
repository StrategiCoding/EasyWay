using EasyWay.Internals;
using EasyWay.Internals.RefreshTokens;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay
{
    public static class Extensions
    {
        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services)
        {
            services.AddSingleton<IJwtFactory, JwtFactory>();

            services.AddSingleton<IRefreshToken, RefreshToken>();

            return services;
        }
    }
}
