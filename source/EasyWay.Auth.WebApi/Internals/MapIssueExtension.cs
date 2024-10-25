using EasyWay.Internals.Application.Issue;
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
                //TODO Authentication (return userID)
                var userId = new Guid("ca1af613-987b-41df-b82e-ebb6d76a44b8");

                var tokens = await issueTokens.Issue(userId);

                cookie.AddRefreshToken(httpContext, tokens.RefreshToken, tokens.RefreshTokenExpires);

                return new AccessTokenResponse(tokens.AccessToken);
            });
            

            return endpoints;
        }
    }
}
