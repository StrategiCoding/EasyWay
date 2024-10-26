namespace EasyWay.Internals.Domain
{
    internal interface ISecurityTokensRepository
    {
        Task<SecurityTokens?> Get(string refreshToken);

        Task<bool> IfExistsRemove(Guid userId);

        Task<bool> IfExistsRemove(string refreshToken);

        Task Remove(string refreshToken);

        Task Add(SecurityTokens storageTokens);
    }
}
