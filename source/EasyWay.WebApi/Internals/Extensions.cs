using EasyWay.Internals.Authentication;
using EasyWay.Internals.Authorization;
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
            services
                .AddHttpContextAccessor()
                .AddScoped<IUserContext, UserContext>()
                .AddEasyWayAuthentication("XN32ifS0ZumZ0QZTAFyY86GdQRPnTHjwzh42KpflDemEZ+Ewlzpgb3N5l8u9/jWV") //TODO
                .AddEasyWayAuthorization();
        }

        internal static void UseEasyWay(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
