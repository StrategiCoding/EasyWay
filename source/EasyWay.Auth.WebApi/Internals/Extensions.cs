using EasyWay.Internals.Cookies;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals
{
    internal static class Extensions
    {
        internal static IServiceCollection AddAuthWebApi(this IServiceCollection services)
        {
            services.AddSingleton<ICookie, Cookie>();

            return services;
        }
    }
}
