using EasyWay.Internals.AccessTokenCreators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace EasyWay.Internals
{
    internal static class MapIssueExtension
    {
        internal static IEndpointRouteBuilder MapIssueEndpoint(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapPost("/tokens/issue", (IAccessTokensCreator creator) =>
            {
                var token = creator.Create(Guid.NewGuid());

                return new Token(token);
            });

            return endpoints;
        }
    }
}
