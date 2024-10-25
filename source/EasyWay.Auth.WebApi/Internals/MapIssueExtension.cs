using EasyWay.Internals.Cases;
using EasyWay.Internals.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace EasyWay.Internals
{
    internal static class MapIssueExtension
    {
        internal static IEndpointRouteBuilder MapIssueEndpoint(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost(EasyWayAuthApiRoutes.ISSUE_TOKENS, async (IIssueTokens issueTokens, ICookie cookie, HttpContext httpContext) =>
            {
                var userId = Guid.NewGuid();

                var tokens = await issueTokens.Issue(userId);

                cookie.AddRefreshToken(httpContext, tokens.RefreshToken, tokens.RefreshTokenExpires);

                return new AccessTokenResponse(tokens.AccessToken);
            });
            

            return endpoints;
        }
    }
}
