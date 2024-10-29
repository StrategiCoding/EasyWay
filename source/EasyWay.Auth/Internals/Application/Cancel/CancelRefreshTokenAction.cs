using EasyWay.Internals.Contracts;

namespace EasyWay.Internals.Application.Cancel
{
    internal sealed class CancelRefreshTokenAction : ISecurityAction
    {
        internal string? RefreshToken { get; }

        internal CancelRefreshTokenAction(string? refreshToken)
        {
            RefreshToken = refreshToken;
        }
    }
}
