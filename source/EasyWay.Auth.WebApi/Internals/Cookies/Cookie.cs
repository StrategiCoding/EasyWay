﻿using EasyWay.Internals.Settings;
using Microsoft.AspNetCore.Http;

namespace EasyWay.Internals.Cookies
{
    internal sealed class Cookie : ICookie
    {
        private const string _refreshTokenCookieName = "X-Refresh-Token";

        private readonly IAuthServerSettings _authServerSettings;

        public Cookie(IAuthServerSettings authServerSettings) 
        {
            _authServerSettings = authServerSettings;
        }

        public void AddRefreshToken(HttpContext httpContext, string refreshToken, DateTime expires)
        {
            var cookieOptions = new CookieOptions()
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Path = EasyWayAuthApiRoutes.REFRESH_TOKENS,
                Expires = expires,
                IsEssential = true,
                Domain = _authServerSettings.Domain,
                Secure = _authServerSettings.Secure,
            };

            httpContext.Response.Cookies.Append(_refreshTokenCookieName, refreshToken, cookieOptions);
        }

        public string? GetRefreshToken(HttpContext httpContext)
        {
            return httpContext.Request.Cookies.SingleOrDefault(x => x.Key == _refreshTokenCookieName).Value;
        }
    }
}