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
            services.AddSingleton<ITokensCreator, TokensCreator>();

            services.AddSingleton<IRefreshToken, RefreshToken>();

            return services;
        }
    }
}
