using EasyWay.Internals.AccessTokenCreators;
using EasyWay.Internals.Application.Cancel;
using EasyWay.Internals.Application.Issue;
using EasyWay.Internals.Application.Refresh;
using EasyWay.Internals.Contracts;
using EasyWay.Internals.Domain;
using EasyWay.Internals.Infrastructure;
using EasyWay.Internals.RefreshTokenCreators;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals
{
    internal static class Extensions
    {
        internal static IServiceCollection AddAuth(this IServiceCollection services)
        {
            services.AddSecurityActions();

            services.AddSingleton<IAccessTokensCreator, AccessTokensCreator>();
            services.AddSingleton<IRefreshTokenCreator, RefreshTokenCreator>();
            services.AddSingleton<IRefreshTokenHasher, RefreshTokenHasher>();

            services.AddScoped<IIssueTokens, IssueTokens>();
            services.AddScoped<IRefreshTokens, RefreshTokens>();
            services.AddScoped<ICancelRefreshToken, CancelRefreshToken>();

            services.AddScoped<ISecurityTokensRepository, SecurityTokensRepository>();

            return services;
        }
    }
}
