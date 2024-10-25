using Microsoft.AspNetCore.Http;

namespace EasyWay.Internals.Cookies
{
    internal sealed class Cookie : ICookie
    {
        private const string _refreshTokenCookieName = "RefreshToken";

        public void AddRefreshToken(HttpContext httpContext, string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions()
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Path = EasyWayAuthApiRoutes.REFRESH_TOKENS,
                Expires = expires,
                IsEssential = true,
                //TODO Secure = true,
                //TODO Domain appsettings
            };

            httpContext.Response.Cookies.Append(_refreshTokenCookieName, refreshToken, cookieOptions);
        }

        public string? GetRefreshToken(HttpContext httpContext)
        {
            return httpContext.Request.Cookies.SingleOrDefault(x => x.Key == _refreshTokenCookieName).Value;
        }
    }
}
