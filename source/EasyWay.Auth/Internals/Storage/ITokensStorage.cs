namespace EasyWay.Internals.Storage
{
    internal interface ITokensStorage
    {
        Task<StorageTokens?> Get(string refreshToken);

        Task<bool> Exists(Guid userId);

        Task Remove(Guid userId);

        Task Add(StorageTokens storageTokens);
    }
}
