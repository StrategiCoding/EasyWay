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

        internal static StorageTokens Issue(Guid userId, string hashedRefreshToken, TimeSpan refreshTokenLifetime, DateTime accessTokenExpires)
        {
            var refreshTokenExpires = DateTime.UtcNow.Add(refreshTokenLifetime);

            return new StorageTokens(userId, hashedRefreshToken, refreshTokenExpires, accessTokenExpires);
        }

        internal void Refresh(string refreshToken, DateTime accessTokenExpires)
        {
            if(!IsAccessTokenExpired())
            {
                throw new Exception("Access token is not expired");
            }

            if (IsRefreshTokenExpired())
            {
                throw new Exception("Refresh token is expired");
            }

            HashedRefreshToken = refreshToken;
            AccessTokenExpires = accessTokenExpires;
        }

        private bool IsAccessTokenExpired() => DateTime.UtcNow >= AccessTokenExpires;

        private bool IsRefreshTokenExpired() => DateTime.UtcNow >= RefreshTokenExpires;
    }
}
