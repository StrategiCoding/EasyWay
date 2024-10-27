using EasyWay.Internals.Domain.SeedWorks.Results;

namespace EasyWay.Internals.Application.Issue
{
    internal interface IIssueTokens
    {
        Task<SecurityResult<TokensDto>> Issue(Guid userId);
    }
}
