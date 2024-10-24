namespace EasyWay.Internals.Storage
{
    internal sealed class TokenStorageModel
    {
        internal string RefreshToken { get; private set; }

        internal DateTime ExpirationRefreshToken { get; private set; }

        internal string AccessToken { get; private set; }

        internal DateTime ExpirationAccessToken { get; private set; }
    }
}
