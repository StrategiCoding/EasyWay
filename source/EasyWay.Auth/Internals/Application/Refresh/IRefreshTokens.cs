namespace EasyWay.Internals.Application.Refresh
{
    internal interface IRefreshTokens
    {
        Task<TokensDto> Refresh(string? oldRefreshToken);
    }
}
