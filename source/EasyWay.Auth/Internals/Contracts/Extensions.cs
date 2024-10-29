using EasyWay.Internals.Application.Refresh;
using EasyWay.Internals.Application;
using Microsoft.Extensions.DependencyInjection;
using EasyWay.Internals.Application.Issue;
using EasyWay.Internals.Application.Cancel;

namespace EasyWay.Internals.Contracts
{
    internal static class Extensions
    {
        internal static IServiceCollection AddSecurityActions(this IServiceCollection services)
        {
            services.AddScoped<ISecurityActionExecutor, SecurityActionExecutor>();

            services.AddScoped<ISecurityActionHandler<RefreshTokensAction, TokensDto>, RefreshTokensActionHandler>();
            services.AddScoped<ISecurityActionHandler<IssueTokensAction, TokensDto>, IssueTokensActionHandler>();
            services.AddScoped<ISecurityActionHandler<CancelRefreshTokenAction>, CancelRefreshTokenActionHandler>();

            return services;
        }
    }
}
