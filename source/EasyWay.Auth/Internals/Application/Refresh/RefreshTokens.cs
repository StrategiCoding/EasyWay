using EasyWay.Internals.AccessTokenCreators;
using EasyWay.Internals.Domain;
using EasyWay.Internals.Domain.SeedWorks;
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

        public async Task<SecurityResult<TokensDto>> Refresh(string? oldRefreshToken)
        {
            if (string.IsNullOrEmpty(oldRefreshToken))
            {
                return SecurityResult<TokensDto>.Failure(new RefreshTokenIsNotProvidedSecurityError());
            }

            var storageTokens = await _storage.Get(oldRefreshToken);

            if (storageTokens is null)
            {
                return SecurityResult<TokensDto>.Failure(new RefreshTokenDoesNotExistSecurityError());
            }

            var accessToken = _accessTokensCreator.Create(storageTokens.UserId);

            var newRefreshToken = _refreshTokenCreator.Create();

            var refreshResult = storageTokens.Refresh(newRefreshToken, accessToken.Expires);

            if (refreshResult.IsFailure)
            {
                return SecurityResult<TokensDto>.Failure(refreshResult.Error);
            }

            return SecurityResult<TokensDto>.Success(new TokensDto(newRefreshToken, storageTokens.RefreshTokenExpires, accessToken.Token));
        }
    }
}
