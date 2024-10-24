using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace EasyWay.Internals
{
    internal static class MapIssueTokensExtensions
    {
        internal static IEndpointConventionBuilder MapIssueTokens(this IEndpointRouteBuilder endpoints, string method)
        {
            return endpoints.MapPost("tokens/issue/{string:method}", async (CancellationToken cancellationToken) =>
            {
                //TODO znajdź metode


            });
        }
    }
    // exchange*
    // refresh
    // cancel*
}
