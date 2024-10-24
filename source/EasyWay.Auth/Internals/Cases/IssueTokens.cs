using EasyWay.Internals.AccessTokenCreators;
using EasyWay.Internals.RefreshTokenCreators;
using EasyWay.Internals.Storage;

namespace EasyWay.Internals.Cases
{
    internal sealed class IssueTokens : IIssueTokens
    {
        private readonly IAccessTokensCreator _accessTokensCreator;

        private readonly IRefreshTokenCreator _refreshTokenCreator;

        private readonly ITokensStorage _storage;

        public IssueTokens(
            IAccessTokensCreator accessTokensCreator,
            IRefreshTokenCreator refreshTokenCreator,
            ITokensStorage storage)
        {
            _accessTokensCreator = accessTokensCreator;
            _refreshTokenCreator = refreshTokenCreator;
            _storage = storage;
        }

        public async Task<Tokens> Issue(Guid userId)
        {
            //TODO how many tokens per user ? logout or overrite
            var accessToken = _accessTokensCreator.Create(userId);

            //TODO hash
            var refreshToken = _refreshTokenCreator.Create();

            //TODO expiration
            //TODO hash after check rules
            var storageToken = StorageTokens.Issue(userId, refreshToken, DateTime.UtcNow.AddDays(7), DateTime.UtcNow.AddMinutes(1));

            await _storage.Add(storageToken);

            return new Tokens(refreshToken, storageToken.RefreshTokenExpires, accessToken);
        }
    }
}
