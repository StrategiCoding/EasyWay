using EasyWay.Internals.Domain.SeedWorks;

namespace EasyWay.Internals.Application.Refresh
{
    internal interface IRefreshTokens
    {
        Task<SecurityResult<TokensDto>> Refresh(string? oldRefreshToken);
    }
}
