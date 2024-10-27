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

                var tokensResult = await refreshTokens.Refresh(oldRefreshToken);

                if (tokensResult.IsFailure)
                {
                    cookie.Remove(httpContext);

                    return Results.StatusCode(401);
                }

                cookie.Add(httpContext, tokensResult.Value.RefreshToken, tokensResult.Value.RefreshTokenExpires);

                return Results.Ok(new AccessTokenResponse(tokensResult.Value.AccessToken));
            });

            return endpoints;
        }
    }
}
