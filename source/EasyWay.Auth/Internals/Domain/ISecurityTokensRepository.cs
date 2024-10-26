namespace EasyWay.Internals.Domain
{
    internal interface ISecurityTokensRepository
    {
        Task<SecurityTokens?> Get(string refreshToken);

        Task<bool> IfExistsRemove(Guid userId);

        Task Remove(string refreshToken);

        Task Add(SecurityTokens storageTokens);
    }
}
