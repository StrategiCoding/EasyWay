using EasyWay.Internals.Cookies;
using EasyWay.Internals.Settings;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals
{
    internal static class Extensions
    {
        internal static IServiceCollection AddAuthWebApi(this IServiceCollection services)
        {
            services.AddSingleton<ICookie, Cookie>();

            services.AddSingleton<IAuthServerSettings>(new AuthServerSettings());

            return services;
        }
    }
}
