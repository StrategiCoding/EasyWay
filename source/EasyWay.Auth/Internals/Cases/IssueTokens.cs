using EasyWay.Internals.AccessTokenCreators;
using EasyWay.Internals.RefreshTokenCreators;
using EasyWay.Internals.Storage;
using EasyWay.Settings;

namespace EasyWay.Internals.Cases
{
    internal sealed class IssueTokens : IIssueTokens
    {
        private readonly IAccessTokensCreator _accessTokensCreator;

        private readonly IRefreshTokenCreator _refreshTokenCreator;

        private readonly ITokensStorage _storage;

        private readonly IAuthSettings _authSettings;

        public IssueTokens(
            IAccessTokensCreator accessTokensCreator,
            IRefreshTokenCreator refreshTokenCreator,
            ITokensStorage storage,
            IAuthSettings authSettings)
        {
            _accessTokensCreator = accessTokensCreator;
            _refreshTokenCreator = refreshTokenCreator;
            _storage = storage;
            _authSettings = authSettings;
        }

        public async Task<Tokens> Issue(Guid userId)
        {
            if (await _storage.Exists(userId))
            {
                await _storage.Remove(userId);

                throw new Exception("EXISTS");
            }

            var accessToken = _accessTokensCreator.Create(userId);

            //TODO hash
            var refreshToken = _refreshTokenCreator.Create();

            //TODO expiration
            //TODO hash after check rules
            var storageToken = StorageTokens.Issue(userId, refreshToken, _authSettings.RefreshTokenLifetime, accessToken.Expires);

            await _storage.Add(storageToken);

            return new Tokens(refreshToken, storageToken.RefreshTokenExpires, accessToken.Token);
        }
    }
}
