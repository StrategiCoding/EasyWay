using EasyWay.Internals.Contracts;

namespace EasyWay.Internals.Application.Issue
{
    internal sealed class IssueTokensAction : ISecurityAction<TokensDto>
    {
        internal Guid UserId { get;  }

        internal IssueTokensAction(Guid userId)
        {
            UserId = userId;
        }
    }
}
