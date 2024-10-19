using EasyWay.Internals;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay
{
    public static class Extensions
    {
        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services)
        {
            services.AddSingleton<IJwtFactory, JwtFactory>();

            return services;
        }
    }
}
