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

        public async Task<Tokens> Refresh(string? oldRefreshToken)
        {
            //TODO null or empty
            if(string.IsNullOrEmpty(oldRefreshToken))
            {
                //TODO Forbidden
                throw new ArgumentNullException(nameof(oldRefreshToken));
            }

            var storageTokens = await _storage.Get(oldRefreshToken);

            //TODO check refresh token does not expire
            //TODO check access token expired

            var accessToken = _accessTokensCreator.Create(storageTokens.UserId);

            var newRefreshToken = _refreshTokenCreator.Create();

            storageTokens.Refresh(newRefreshToken, accessToken.Expires);

            //TODO check expiration date refresh and access token ?

            return new Tokens(newRefreshToken, storageTokens.RefreshTokenExpires ,accessToken.Token);
        }
    }
}
