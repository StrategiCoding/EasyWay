using EasyWay.Internals.AccessTokenCreators;
using EasyWay.Internals.Contracts;
using EasyWay.Internals.Domain;
using EasyWay.Internals.Domain.SeedWorks.Results;
using EasyWay.Internals.RefreshTokenCreators;
using EasyWay.Settings;
using Microsoft.Extensions.Logging;

namespace EasyWay.Internals.Application.Issue
{
    internal sealed class IssueTokensActionHandler : ISecurityActionHandler<IssueTokensAction, TokensDto>
    {
        private readonly IAccessTokensCreator _accessTokensCreator;

        private readonly IRefreshTokenCreator _refreshTokenCreator;

        private readonly ISecurityTokensRepository _storage;

        private readonly IAuthSettings _authSettings;

        private readonly IRefreshTokenHasher _refreshTokenHasher;

        private readonly ILogger<IssueTokensAction> _logger;

        public IssueTokensActionHandler(
            IAccessTokensCreator accessTokensCreator,
            IRefreshTokenCreator refreshTokenCreator,
            ISecurityTokensRepository storage,
            IAuthSettings authSettings,
            IRefreshTokenHasher refreshTokenHasher,
            ILogger<IssueTokensAction> logger)
        {
            _accessTokensCreator = accessTokensCreator;
            _refreshTokenCreator = refreshTokenCreator;
            _storage = storage;
            _authSettings = authSettings;
            _refreshTokenHasher = refreshTokenHasher;
            _logger = logger;
        }

        public async Task<SecurityResult<TokensDto>> Handle(IssueTokensAction action)
        {
            if (await _storage.IfExistsRemove(action.UserId))
            {
                _logger.LogInformation("Overwritten refresh token for user {@userId}", action.UserId);
            }

            //TODO hash
            var refreshToken = _refreshTokenCreator.Create();
            var accessToken = _accessTokensCreator.Create(action.UserId);

            var hashedRefreshToken = _refreshTokenHasher.Hash(refreshToken);

            //TODO hash after check rules
            var storageToken = SecurityTokens.Issue(action.UserId, hashedRefreshToken, _authSettings.RefreshTokenLifetime, accessToken.Expires);

            await _storage.Add(storageToken);

            return SecurityResult<TokensDto>.Success(new TokensDto(refreshToken, storageToken.RefreshTokenExpires, accessToken.Token));
        }
    }
}
