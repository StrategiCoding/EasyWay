namespace EasyWay.Internals.Domain
{
    internal interface ISecurityTokensRepository
    {
        Task<SecurityTokens?> Get(string refreshToken);

        Task<bool> Exists(Guid userId);

        Task Remove(Guid userId);

        Task Add(SecurityTokens storageTokens);
    }
}
