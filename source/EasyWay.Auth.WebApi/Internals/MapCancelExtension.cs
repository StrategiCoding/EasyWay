using EasyWay.Internals.Application.Cancel;
using EasyWay.Internals.Contracts;
using EasyWay.Internals.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EasyWay.Internals
{
    internal static class MapCancelExtension
    {
        internal static IEndpointRouteBuilder MapCancelEndpoint(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost(EasyWayAuthApiRoutes.CANCEL_TOKENS, async (
                ISecurityActionExecutor executor,
                IRefreshTokenCookie refreshTokenCookie,
                HttpContext httpContext) =>
            {
                var refreshToken = refreshTokenCookie.Get(httpContext);

                var tokensResult = await executor.Execute(new CancelRefreshTokenAction(refreshToken));

                refreshTokenCookie.Remove(httpContext);

                return tokensResult.IsSuccess ? Results.StatusCode(200) : Results.StatusCode(401);
            });

            return endpoints;
        }
    }
}
