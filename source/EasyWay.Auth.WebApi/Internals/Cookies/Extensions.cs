using Microsoft.AspNetCore.Http;

namespace EasyWay.Internals.Cookies
{
    internal static class Extensions
    {
        private const string _refreshTokenCookieName = "RefreshToken";

        internal static void AddRefreshToken(this HttpContext httpContext, string refreshToken, DateTime expires)
        {
            CookieOptions _cookieOptions = new CookieOptions()
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Path = EasyWayAuthApiRoutes.REFRESH_TOKENS,
                Expires = expires,
                IsEssential = true,
                //TODO Secure = true,
                //TODO Domain appsettings
            };

            httpContext.Response.Cookies.Append(_refreshTokenCookieName, refreshToken, _cookieOptions);
        }

        internal static string GetRefreshToken(this HttpContext httpContext)
        {
            return httpContext.Request.Cookies.SingleOrDefault(x => x.Key == _refreshTokenCookieName).Value;
        }
    }
}
