using EasyWay.Internals.Contexts;
using EasyWay.Internals.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals
{
    internal static class Extensions
    {
        internal static void AddEasyWayWebApi(
            this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddScoped<IUserContext, UserContext>();
        }

        internal static void UseEasyWay(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
