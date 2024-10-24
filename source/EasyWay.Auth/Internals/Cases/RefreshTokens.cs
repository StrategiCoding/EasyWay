using EasyWay.Internals.AccessTokenCreators;
using EasyWay.Internals.RefreshTokenCreators;
using EasyWay.Internals.Storage;

namespace EasyWay.Internals.Cases
{
    internal sealed class RefreshTokens : IRefreshTokens
    {
        private readonly IAccessTokensCreator _accessTokensCreator;

        private readonly IRefreshTokenCreator _refreshTokenCreator;

        private readonly ITokensStorage _storage;

        public RefreshTokens(
            IAccessTokensCreator accessTokensCreator,
            IRefreshTokenCreator refreshTokenCreator,
            ITokensStorage storage)
        {
            _accessTokensCreator = accessTokensCreator;
            _refreshTokenCreator = refreshTokenCreator;
            _storage = storage;
        }

        public async Task<Tokens> Refresh(string oldRefreshToken)
        {
            var storageTokens = await _storage.Get(oldRefreshToken);

            //TODO check refresh token does not expire
            //TODO check access token expired

            var newAccessToken = _accessTokensCreator.Create(storageTokens.UserId);

            var newRefreshToken = _refreshTokenCreator.Create();

            //TODO Expires
            storageTokens.Refresh(newRefreshToken, DateTime.UtcNow.AddMinutes(10));

            //TODO check expiration date refresh and access token ?

            return new Tokens(newRefreshToken, storageTokens.RefreshTokenExpires ,newAccessToken);
        }
    }
}
