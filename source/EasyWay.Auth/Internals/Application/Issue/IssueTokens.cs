using EasyWay.Internals.AccessTokenCreators;
using EasyWay.Internals.Domain;
using EasyWay.Internals.Domain.SeedWorks;
using EasyWay.Internals.RefreshTokenCreators;
using EasyWay.Settings;
using Microsoft.Extensions.Logging;

namespace EasyWay.Internals.Application.Issue
{
    internal sealed class IssueTokens : IIssueTokens
    {
        private readonly IAccessTokensCreator _accessTokensCreator;

        private readonly IRefreshTokenCreator _refreshTokenCreator;

        private readonly ISecurityTokensRepository _storage;

        private readonly ILogger<IssueTokens> _logger;

        private readonly IAuthSettings _authSettings;

        public IssueTokens(
            IAccessTokensCreator accessTokensCreator,
            IRefreshTokenCreator refreshTokenCreator,
            ISecurityTokensRepository storage,
            ILogger<IssueTokens> logger,
            IAuthSettings authSettings)
        {
            _accessTokensCreator = accessTokensCreator;
            _refreshTokenCreator = refreshTokenCreator;
            _storage = storage;
            _logger = logger;
            _authSettings = authSettings;
        }

        public async Task<SecurityResult<TokensDto>> Issue(Guid userId)
        {
            if (await _storage.IfExistsRemove(userId))
            {
                _logger.LogInformation("Overwritten refresh token for user {@userId}", userId);
            }

            //TODO hash
            var refreshToken = _refreshTokenCreator.Create();
            var accessToken = _accessTokensCreator.Create(userId);

            //TODO hash after check rules
            var storageToken = SecurityTokens.Issue(userId, refreshToken, _authSettings.RefreshTokenLifetime, accessToken.Expires);

            await _storage.Add(storageToken);

            return SecurityResult<TokensDto>.Success(new TokensDto(refreshToken, storageToken.RefreshTokenExpires, accessToken.Token));
        }
    }
}
