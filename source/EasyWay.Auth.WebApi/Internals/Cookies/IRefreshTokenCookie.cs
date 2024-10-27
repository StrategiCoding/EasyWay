using Microsoft.AspNetCore.Http;

namespace EasyWay.Internals.Cookies
{
    internal interface IRefreshTokenCookie
    {
        string? Get(HttpContext httpContext);

        void Add(HttpContext httpContext, string refreshToken, DateTime expires);

        void Remove(HttpContext httpContext);
    }
}
