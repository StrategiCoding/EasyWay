namespace EasyWay.Internals.Application.Issue
{
    internal interface IIssueTokens
    {
        Task<TokensDto> Issue(Guid userId);
    }
}
