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

        public DateTime? RefreshTokenTimeout { get; private set; }

        public DateTime AccessTokenExpires { get; private set; }

        private SecurityTokens(
            Guid userId,
            string hashedRefreshToken,
            DateTime refreshTokenExpires,
            DateTime? refreshTokenTimeout,
            DateTime accessTokenExpires)
        {
            UserId = userId;
            HashedRefreshToken = hashedRefreshToken;
            RefreshTokenExpires = refreshTokenExpires;
            RefreshTokenTimeout = refreshTokenTimeout;
            AccessTokenExpires = accessTokenExpires;
        }

        internal static SecurityTokens Issue(
            Guid userId,
            string hashedRefreshToken,
            TimeSpan refreshTokenLifetime,
            TimeSpan? refreshTokenMaxIdleTime,
            DateTime accessTokenExpires)
        {
            var refreshTokenExpires = SecurityClock.UtcNow.Add(refreshTokenLifetime);
            var refreshTokenTimeout = CalculateRefreshTokenTimeout(refreshTokenMaxIdleTime, refreshTokenExpires);

            return new SecurityTokens(userId, hashedRefreshToken, refreshTokenExpires, refreshTokenTimeout, accessTokenExpires);
        }

        internal SecurityResult Refresh(
            string refreshToken,
            DateTime accessTokenExpires,
            IRefreshTokenHasher refreshTokenHasher,
            TimeSpan? refreshTokenMaxIdleTime)
        {
            if (!IsAccessTokenExpired())
            {
                return SecurityResult.Failure(new AccessTokenIsNotExpiredSecurityError());
            }

            if (IsRefreshTokenExpired())
            {
                return SecurityResult.Failure(new RefreshTokenIsExpiredSecurityError());
            }

            if (IsRefreshTokenIdleTimeExceeded())
            {
                return SecurityResult.Failure(new RefreshTokenTimeoutExceededSecurityError());
            }

            RefreshTokenTimeout = CalculateRefreshTokenTimeout(refreshTokenMaxIdleTime, RefreshTokenExpires);
            HashedRefreshToken = refreshTokenHasher.Hash(refreshToken);
            AccessTokenExpires = accessTokenExpires;

            return SecurityResult.Success;
        }

        private bool IsAccessTokenExpired() => SecurityClock.UtcNow >= AccessTokenExpires;

        private bool IsRefreshTokenExpired() => SecurityClock.UtcNow >= RefreshTokenExpires;

        private bool IsRefreshTokenIdleTimeExceeded() => RefreshTokenTimeout is not null ? SecurityClock.UtcNow >= RefreshTokenTimeout : false;

        private static DateTime? CalculateRefreshTokenTimeout(TimeSpan? refreshTokenMaxIdleTime, DateTime refreshTokenExpires)
        {
            DateTime? refreshTokenTimeout = refreshTokenMaxIdleTime.HasValue ? SecurityClock.UtcNow + refreshTokenMaxIdleTime.Value : null;

            if (refreshTokenTimeout.HasValue)
            {
                return refreshTokenTimeout > refreshTokenExpires ? refreshTokenExpires : refreshTokenTimeout;
            }

            return refreshTokenTimeout;
        }
    }
}
