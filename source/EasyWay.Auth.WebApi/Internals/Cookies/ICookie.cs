using Microsoft.AspNetCore.Http;

namespace EasyWay.Internals.Cookies
{
    internal interface ICookie
    {
        string? GetRefreshToken(HttpContext httpContext);

        void AddRefreshToken(HttpContext httpContext, string refreshToken, DateTime expires);
    }
}
