using EasyWay.Internals.Contracts;

namespace EasyWay.Internals.Application.Refresh
{
    internal sealed class RefreshTokensAction : ISecurityAction<TokensDto>
    {
        internal string? OldRefreshToken { get; }

        internal RefreshTokensAction(string? oldRefreshToken)
        {
            OldRefreshToken = oldRefreshToken;
        }
    }
}
