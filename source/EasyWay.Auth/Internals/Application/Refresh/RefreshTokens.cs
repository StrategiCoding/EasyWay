using EasyWay.Internals.AccessTokenCreators;
using EasyWay.Internals.Domain;
using EasyWay.Internals.RefreshTokenCreators;

namespace EasyWay.Internals.Application.Refresh
{
    internal sealed class RefreshTokens : IRefreshTokens
    {
        private readonly IAccessTokensCreator _accessTokensCreator;

        private readonly IRefreshTokenCreator _refreshTokenCreator;

        private readonly ISecurityTokensRepository _storage;

        public RefreshTokens(
            IAccessTokensCreator accessTokensCreator,
            IRefreshTokenCreator refreshTokenCreator,
            ISecurityTokensRepository storage)
        {
            _accessTokensCreator = accessTokensCreator;
            _refreshTokenCreator = refreshTokenCreator;
            _storage = storage;
        }

        public async Task<TokensDto> Refresh(string? oldRefreshToken)
        {
            if (string.IsNullOrEmpty(oldRefreshToken))
            {
                throw new RefreshTokenCannotBeNullOrEmptyException();
            }

            var storageTokens = await _storage.Get(oldRefreshToken);

            var accessToken = _accessTokensCreator.Create(storageTokens.UserId);

            var newRefreshToken = _refreshTokenCreator.Create();

            storageTokens.Refresh(newRefreshToken, accessToken.Expires);

            return new TokensDto(newRefreshToken, storageTokens.RefreshTokenExpires, accessToken.Token);
        }
    }
}
