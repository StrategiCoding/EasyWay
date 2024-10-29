using EasyWay.Internals.Contracts;
using EasyWay.Internals.Domain;
using EasyWay.Internals.Domain.SeedWorks.Results;
using EasyWay.Internals.RefreshTokenCreators;

namespace EasyWay.Internals.Application.Cancel
{
    internal sealed class CancelRefreshTokenActionHandler : ISecurityActionHandler<CancelRefreshTokenAction>
    {
        private readonly ISecurityTokensRepository _repository;

        private readonly IRefreshTokenHasher _refreshTokenHasher;

        public CancelRefreshTokenActionHandler(
            ISecurityTokensRepository repository,
            IRefreshTokenHasher refreshTokenHasher)
        {
            _repository = repository;
            _refreshTokenHasher = refreshTokenHasher;
        }

        public async Task<SecurityResult> Handle(CancelRefreshTokenAction action)
        {
            if (string.IsNullOrEmpty(action.RefreshToken))
            {
                return SecurityResult.Failure(new RefreshTokenIsNotProvidedSecurityError());
            }

            var hashedRefreshToken = _refreshTokenHasher.Hash(action.RefreshToken);

            if (!await _repository.IfExistsRemove(hashedRefreshToken))
            {
                return SecurityResult.Failure(new RefreshTokenDoesNotExistSecurityError());
            }

            return SecurityResult.Success;
        }
    }
}
