using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Authorization
{
    internal static class Extensions
    {
        internal static IServiceCollection AddEasyWayAuthorization(
            this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.FallbackPolicy = new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .Build();
            });

            return services;
        }
    }
}
