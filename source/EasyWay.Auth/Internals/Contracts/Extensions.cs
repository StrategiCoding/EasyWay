using EasyWay.Internals.Application.Refresh;
using EasyWay.Internals.Application;
using Microsoft.Extensions.DependencyInjection;
using EasyWay.Internals.Application.Issue;

namespace EasyWay.Internals.Contracts
{
    internal static class Extensions
    {
        internal static IServiceCollection AddSecurityActions(this IServiceCollection services)
        {
            services.AddScoped<ISecurityActionExecutor, SecurityActionExecutor>();

            services.AddScoped<ISecurityActionHandler<RefreshTokensAction, TokensDto>, RefreshTokensActionHandler>();
            services.AddScoped<ISecurityActionHandler<IssueTokensAction, TokensDto>, IssueTokensActionHandler>();

            return services;
        }
    }
}
