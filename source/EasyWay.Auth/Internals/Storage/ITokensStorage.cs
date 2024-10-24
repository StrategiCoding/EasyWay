namespace EasyWay.Internals.Storage
{
    internal interface ITokensStorage
    {
        Task<StorageTokens?> Get(string refreshToken);

        Task Add(StorageTokens storageTokens);
    }
}
