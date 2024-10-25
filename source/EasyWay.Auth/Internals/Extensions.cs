using EasyWay.Internals.AccessTokenCreators;
using EasyWay.Internals.Application.Issue;
using EasyWay.Internals.Application.Refresh;
using EasyWay.Internals.Domain;
using EasyWay.Internals.Infrastructure;
using EasyWay.Internals.RefreshTokenCreators;
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

            services.AddScoped<ISecurityTokensRepository, SecurityTokensRepository>();

            services.AddSingleton<IAuthSettings>(new AuthSettings());

            return services;
        }
    }
}
