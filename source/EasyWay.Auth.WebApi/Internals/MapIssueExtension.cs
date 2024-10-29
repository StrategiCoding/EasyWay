using EasyWay.Internals.Application;
using EasyWay.Internals.Application.Issue;
using EasyWay.Internals.Contracts;
using EasyWay.Internals.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace EasyWay.Internals
{
    internal static class MapIssueExtension
    {
        internal static IEndpointRouteBuilder MapIssueEndpoint(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost(EasyWayAuthApiRoutes.ISSUE_TOKENS, async (
                ISecurityActionExecutor executor,
                IRefreshTokenCookie cookie,
                HttpContext httpContext) =>
            {
                //TODO Authentication (return userID)
                var userId = new Guid("ca1af613-987b-41df-b82e-ebb6d76a44b8");

                var issueTokensResult = await executor.Execute<IssueTokensAction, TokensDto>(new IssueTokensAction(userId));

                if (issueTokensResult.IsFailure)
                {
                    cookie.Remove(httpContext);

                    return Results.StatusCode(401);
                }

                cookie.Add(httpContext, issueTokensResult.Value.RefreshToken, issueTokensResult.Value.RefreshTokenExpires);

                return Results.Ok(new AccessTokenResponse(issueTokensResult.Value.AccessToken));
            });
            

            return endpoints;
        }
    }
}
