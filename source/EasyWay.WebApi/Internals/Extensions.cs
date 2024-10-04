using EasyWay.Internals.Exceptions;
using Microsoft.AspNetCore.Builder;

namespace EasyWay.Internals
{
    internal static class Extensions
    {
        internal static void UseEasyWay(this IApplicationBuilder app)
        {
            app.UseHttpsRedirection();
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
