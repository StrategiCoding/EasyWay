namespace EasyWay.Internals.Cases
{
    internal interface IIssueTokens
    {
        Task<Tokens> Issue(Guid userId);
    }
}
