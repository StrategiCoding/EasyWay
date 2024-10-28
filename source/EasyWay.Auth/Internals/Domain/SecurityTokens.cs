using EasyWay.Internals.Domain.Errors;
using EasyWay.Internals.Domain.SeedWorks.Clocks;
using EasyWay.Internals.Domain.SeedWorks.Results;
using EasyWay.Internals.RefreshTokenCreators;

namespace EasyWay.Internals.Domain
{
    internal sealed class SecurityTokens
    {
        public Guid UserId { get; private set; }

        public string HashedRefreshToken { get; private set; }

        public DateTime RefreshTokenExpires { get; private set; }

        public DateTime AccessTokenExpires { get; private set; }

        private SecurityTokens(Guid userId, string hashedRefreshToken, DateTime refreshTokenExpires, DateTime accessTokenExpires)
        {
            UserId = userId;
            HashedRefreshToken = hashedRefreshToken;
            RefreshTokenExpires = refreshTokenExpires;
            AccessTokenExpires = accessTokenExpires;
        }

        internal static SecurityTokens Issue(Guid userId, string hashedRefreshToken, TimeSpan refreshTokenLifetime, DateTime accessTokenExpires)
        {
            var refreshTokenExpires = SecurityClock.UtcNow.Add(refreshTokenLifetime);

            return new SecurityTokens(userId, hashedRefreshToken, refreshTokenExpires, accessTokenExpires);
        }

        internal SecurityResult Refresh(string refreshToken, DateTime accessTokenExpires, IRefreshTokenHasher refreshTokenHasher)
        {
            if (!IsAccessTokenExpired())
            {
                return SecurityResult.Failure(new AccessTokenIsNotExpiredException());
            }

            if (IsRefreshTokenExpired())
            {
                return SecurityResult.Failure(new RefreshTokenIsExpiredException());
            }

            HashedRefreshToken = refreshTokenHasher.Hash(refreshToken);
            AccessTokenExpires = accessTokenExpires;

            return SecurityResult.Success;
        }

        private bool IsAccessTokenExpired() => SecurityClock.UtcNow >= AccessTokenExpires;

        private bool IsRefreshTokenExpired() => SecurityClock.UtcNow >= RefreshTokenExpires;
    }
}
