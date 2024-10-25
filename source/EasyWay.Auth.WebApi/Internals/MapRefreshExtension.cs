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
            endpoints.MapPost(EasyWayAuthApiRoutes.REFRESH_TOKENS, async (IRefreshTokens refreshTokens, IRefreshTokenCookie cookie, HttpContext httpContext) =>
            {
                var oldRefreshToken = cookie.Get(httpContext);

                var tokens = await refreshTokens.Refresh(oldRefreshToken);

                cookie.Add(httpContext, tokens.RefreshToken, tokens.RefreshTokenExpires);

                // TODO remove refresh token when forbidden
                // cookie.Remove(httpContext);

                return new AccessTokenResponse(tokens.AccessToken);
            });

            return endpoints;
        }
    }
}
