using EasyWay.Internals.WebApi;
using Microsoft.AspNetCore.Builder;

namespace EasyWay
{
    public static class ExtensionUseEasyWay
    {
        public static void UseEasyWay(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
