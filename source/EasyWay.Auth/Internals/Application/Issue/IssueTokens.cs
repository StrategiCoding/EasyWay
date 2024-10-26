using EasyWay.Internals.AccessTokenCreators;
using EasyWay.Internals.Domain;
using EasyWay.Internals.RefreshTokenCreators;
using EasyWay.Settings;

namespace EasyWay.Internals.Application.Issue
{
    internal sealed class IssueTokens : IIssueTokens
    {
        private readonly IAccessTokensCreator _accessTokensCreator;

        private readonly IRefreshTokenCreator _refreshTokenCreator;

        private readonly ISecurityTokensRepository _storage;

        private readonly IAuthSettings _authSettings;

        public IssueTokens(
            IAccessTokensCreator accessTokensCreator,
            IRefreshTokenCreator refreshTokenCreator,
            ISecurityTokensRepository storage,
            IAuthSettings authSettings)
        {
            _accessTokensCreator = accessTokensCreator;
            _refreshTokenCreator = refreshTokenCreator;
            _storage = storage;
            _authSettings = authSettings;
        }

        public async Task<TokensDto> Issue(Guid userId)
        {
            if (await _storage.Exists(userId))
            {
                await _storage.Remove(userId);

                throw new RefreshTokenIsValidException();
            }

            var accessToken = _accessTokensCreator.Create(userId);

            //TODO hash
            var refreshToken = _refreshTokenCreator.Create();

            //TODO expiration
            //TODO hash after check rules
            var storageToken = SecurityTokens.Issue(userId, refreshToken, _authSettings.RefreshTokenLifetime, accessToken.Expires);

            await _storage.Add(storageToken);

            return new TokensDto(refreshToken, storageToken.RefreshTokenExpires, accessToken.Token);
        }
    }
}
