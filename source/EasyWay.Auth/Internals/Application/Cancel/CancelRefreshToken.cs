using EasyWay.Internals.Domain;
using EasyWay.Internals.Domain.SeedWorks;

namespace EasyWay.Internals.Application.Cancel
{
    internal sealed class CancelRefreshToken : ICancelRefreshToken
    {
        private readonly ISecurityTokensRepository _repository;

        public CancelRefreshToken(ISecurityTokensRepository repository) 
        {
            _repository = repository;
        }

        public async Task<SecurityResult> Cancel(string? refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken))
            {
                return SecurityResult.Failure(new RefreshTokenIsNotProvidedSecurityError());
            }

            if (!await _repository.IfExistsRemove(refreshToken))
            {
                return SecurityResult.Failure(new RefreshTokenDoesNotExistSecurityError());
            }

            return SecurityResult.Success;
        }
    }
}
