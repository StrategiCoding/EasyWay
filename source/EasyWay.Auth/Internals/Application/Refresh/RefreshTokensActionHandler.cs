using EasyWay.Internals.AccessTokenCreators;
using EasyWay.Internals.Contracts;
using EasyWay.Internals.Domain;
using EasyWay.Internals.Domain.SeedWorks.Results;
using EasyWay.Internals.RefreshTokenCreators;
using EasyWay.Settings;

namespace EasyWay.Internals.Application.Refresh
{
    internal sealed class RefreshTokensActionHandler : ISecurityActionHandler<RefreshTokensAction, TokensDto>
    {
        private readonly IAccessTokensCreator _accessTokensCreator;

        private readonly IRefreshTokenCreator _refreshTokenCreator;

        private readonly ISecurityTokensRepository _storage;

        private readonly IRefreshTokenHasher _refreshTokenHasher;

        private readonly IAuthSettings _authSettings;

        public RefreshTokensActionHandler(
            IAccessTokensCreator accessTokensCreator,
            IRefreshTokenCreator refreshTokenCreator,
            ISecurityTokensRepository storage,
            IRefreshTokenHasher refreshTokenHasher,
            IAuthSettings authSettings)
        {
            _accessTokensCreator = accessTokensCreator;
            _refreshTokenCreator = refreshTokenCreator;
            _storage = storage;
            _refreshTokenHasher = refreshTokenHasher;
            _authSettings = authSettings;
        }

        public async Task<SecurityResult<TokensDto>> Handle(RefreshTokensAction action)
        {
            if (string.IsNullOrEmpty(action.OldRefreshToken))
            {
                return SecurityResult<TokensDto>.Failure(new RefreshTokenIsNotProvidedSecurityError());
            }

            var hashedOldRefreshToken = _refreshTokenHasher.Hash(action.OldRefreshToken);

            var storageTokens = await _storage.Get(hashedOldRefreshToken);

            if (storageTokens is null)
            {
                return SecurityResult<TokensDto>.Failure(new RefreshTokenDoesNotExistSecurityError());
            }

            var accessToken = _accessTokensCreator.Create(storageTokens.UserId);

            var newRefreshToken = _refreshTokenCreator.Create();

            var refreshResult = storageTokens.Refresh(newRefreshToken, accessToken.Expires, _refreshTokenHasher, _authSettings.RefreshTokenMaxIdleTime);

            if (refreshResult.IsFailure)
            {
                return SecurityResult<TokensDto>.Failure(refreshResult.Error);
            }

            return SecurityResult<TokensDto>.Success(new TokensDto(newRefreshToken, storageTokens.RefreshTokenExpires, accessToken.Token));
        }
    }
}
