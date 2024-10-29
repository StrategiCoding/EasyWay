using EasyWay.Internals.Application.Refresh;
using EasyWay.Internals.Application;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Contracts
{
    internal static class Extensions
    {
        internal static IServiceCollection AddSecurityActions(this IServiceCollection services)
        {
            services.AddScoped<ISecurityActionExecutor, SecurityActionExecutor>();

            services.AddScoped<ISecurityActionHandler<RefreshTokensAction, TokensDto>, RefreshTokensActionHandler>();

            return services;
        }
    }
}
