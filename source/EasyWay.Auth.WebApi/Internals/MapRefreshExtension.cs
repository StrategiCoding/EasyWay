using EasyWay.Internals.Application.Refresh;
using EasyWay.Internals.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EasyWay.Internals
{
    internal static class MapRefreshExtension
    {
        internal static IEndpointRouteBuilder MapRefreshEndpoint(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost(EasyWayAuthApiRoutes.REFRESH_TOKENS, async (IRefreshTokens refreshTokens, ICookie cookie, HttpContext httpContext) =>
            {
                var oldRefreshToken = cookie.GetRefreshToken(httpContext);

                var tokens = await refreshTokens.Refresh(oldRefreshToken);

                cookie.AddRefreshToken(httpContext, tokens.RefreshToken, tokens.RefreshTokenExpires);

                return new AccessTokenResponse(tokens.AccessToken);
            });

            return endpoints;
        }
    }
}
