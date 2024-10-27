using EasyWay.Internals.Domain.SeedWorks.Results;

namespace EasyWay.Internals.Application.Refresh
{
    internal interface IRefreshTokens
    {
        Task<SecurityResult<TokensDto>> Refresh(string? oldRefreshToken);
    }
}
