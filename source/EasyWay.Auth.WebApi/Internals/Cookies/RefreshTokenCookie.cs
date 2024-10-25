using EasyWay.Internals.Settings;
using Microsoft.AspNetCore.Http;

namespace EasyWay.Internals.Cookies
{
    internal sealed class RefreshTokenCookie : IRefreshTokenCookie
    {
        private const string _refreshTokenCookieName = "X-Refresh-Token";

        private readonly IAuthServerSettings _authServerSettings;

        public RefreshTokenCookie(IAuthServerSettings authServerSettings) 
        {
            _authServerSettings = authServerSettings;
        }

        public void Add(HttpContext httpContext, string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.Strict,
                Expires = expires,
                Path = EasyWayAuthApiRoutes.REFRESH_TOKENS,
                Domain = _authServerSettings.Domain,
            };

            httpContext.Response.Cookies.Append(_refreshTokenCookieName, refreshToken, cookieOptions);
        }

        public string? Get(HttpContext httpContext)
        {
            return httpContext.Request.Cookies.SingleOrDefault(x => x.Key == _refreshTokenCookieName).Value;
        }

        public void Remove(HttpContext httpContext)
        {
            var cookieOptions = new CookieOptions()
            {
                HttpOnly = true,
                Secure = true,
                IsEssential = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddMonths(-1),
                Path = EasyWayAuthApiRoutes.REFRESH_TOKENS,
                Domain = _authServerSettings.Domain,
            };

            httpContext.Response.Cookies.Append(_refreshTokenCookieName, string.Empty, cookieOptions);
        }
    }
}
