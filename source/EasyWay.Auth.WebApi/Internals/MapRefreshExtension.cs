using EasyWay.Internals.Cases;
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
            endpoints.MapPost(EasyWayAuthApiRoutes.REFRESH_TOKENS, async (IRefreshTokens refreshTokens, HttpContext context) =>
            {
                var oldRefreshToken = context.GetRefreshToken();

                var tokens = await refreshTokens.Refresh(oldRefreshToken);

                context.AddRefreshToken(tokens.RefreshToken, tokens.RefreshTokenExpires);

                return new AccessTokenResponse(tokens.AccessToken);
            });

            return endpoints;
        }
    }
}
