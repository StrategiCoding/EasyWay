using EasyWay.Internals.Cookies;
using EasyWay.Internals.Settings;
using EasyWay.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals
{
    internal static class Extensions
    {
        internal static IServiceCollection AddAuthWebApi(this IServiceCollection services)
        {
            services.AddSingleton<IRefreshTokenCookie, RefreshTokenCookie>();

            var settings = new AuthServerSettings();

            services.AddSingleton<IAuthServerSettings>(settings);
            services.AddSingleton<IAuthSettings>(settings);

            return services;
        }
    }
}
