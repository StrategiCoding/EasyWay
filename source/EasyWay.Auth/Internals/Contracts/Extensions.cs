using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Contracts
{
    internal static class Extensions
    {
        internal static IServiceCollection AddSecurityActions(this IServiceCollection services)
        {
            services.AddSingleton<ISecurityActionExecutor, SecurityActionExecutor>();


            return services;
        }
    }
}
