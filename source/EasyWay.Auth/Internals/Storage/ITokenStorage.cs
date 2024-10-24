namespace EasyWay.Internals.Storage
{
    internal interface ITokenStorage
    {
        Task Add(TokenStorageModel tokenStorageModel);
    }
}
