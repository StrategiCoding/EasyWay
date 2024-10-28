using EasyWay.Internals.Domain;
using EasyWay.Internals.Domain.SeedWorks.Results;
using EasyWay.Internals.RefreshTokenCreators;

namespace EasyWay.Internals.Application.Cancel
{
    internal sealed class CancelRefreshToken : ICancelRefreshToken
    {
        private readonly ISecurityTokensRepository _repository;

        private readonly IRefreshTokenHasher _refreshTokenHasher;

        public CancelRefreshToken(
            ISecurityTokensRepository repository,
            IRefreshTokenHasher refreshTokenHasher) 
        {
            _repository = repository;
            _refreshTokenHasher = refreshTokenHasher;
        }

        public async Task<SecurityResult> Cancel(string? refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                return SecurityResult.Failure(new RefreshTokenIsNotProvidedSecurityError());
            }

            var hashedRefreshToken = _refreshTokenHasher.Hash(refreshToken);

            if (!await _repository.IfExistsRemove(hashedRefreshToken))
            {
                return SecurityResult.Failure(new RefreshTokenDoesNotExistSecurityError());
            }

            return SecurityResult.Success;
        }
    }
}
