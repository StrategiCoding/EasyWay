using EasyWay.Internals;
using EasyWay.Internals.Contexts;
using EasyWay.Internals.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay
{
    public static class Extensions
    {
        public static void AddEasyWayWebApi(
            this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddSingleton<WebApiResultMapper>();
            services.AddSingleton(typeof(IWebApiModulExecutor<>), typeof(WebApiModulExecutor<>));

            services.AddScoped<IUserContext, UserContext>();
        }

        public static void UseEasyWay(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
