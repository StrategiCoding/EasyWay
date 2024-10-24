namespace EasyWay.Internals.Storage
{
    internal sealed class StorageTokens
    {
        public Guid UserId { get; private set; }

        public string HashedRefreshToken { get; private set; }

        public DateTime RefreshTokenExpires { get; private set; }

        public DateTime AccessTokenExpires { get; private set; }

        private StorageTokens(Guid userId, string hashedRefreshToken, DateTime refreshTokenExpires, DateTime accessTokenExpires) 
        {
            UserId = userId;
            HashedRefreshToken = hashedRefreshToken;
            RefreshTokenExpires = refreshTokenExpires;
            AccessTokenExpires = accessTokenExpires;
        }

        internal static StorageTokens Issue(Guid userId, string hashedRefreshToken, DateTime refreshTokenExpires, DateTime accessTokenExpires)
        {
            return new StorageTokens(userId, hashedRefreshToken, refreshTokenExpires, accessTokenExpires);
        }

        internal void Refresh(string refreshToken, DateTime accessTokenExpires)
        {
            HashedRefreshToken = refreshToken;
            AccessTokenExpires = accessTokenExpires;
        }
    }
}
