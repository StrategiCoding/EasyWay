using EasyWay.Internals.AccessTokenCreators;
using EasyWay.Internals.Cases;
using EasyWay.Internals.RefreshTokenCreators;
using EasyWay.Internals.Storage;
using EasyWay.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals
{
    internal static class Extensions
    {
        internal static IServiceCollection AddAuth(this IServiceCollection services)
        {
            services.AddSingleton<IAccessTokensCreator, AccessTokensCreator>();
            services.AddSingleton<IRefreshTokenCreator, RefreshTokenCreator>();

            services.AddScoped<IIssueTokens, IssueTokens>();
            services.AddScoped<IRefreshTokens, RefreshTokens>();

            services.AddScoped<ITokensStorage, TokensStorage>();

            services.AddSingleton<IAuthSettings>(new AuthSettings());

            return services;
        }
    }
}
