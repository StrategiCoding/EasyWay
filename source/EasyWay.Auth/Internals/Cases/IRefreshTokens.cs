namespace EasyWay.Internals.Cases
{
    internal interface IRefreshTokens
    {
        Task<Tokens> Refresh(string? oldRefreshToken);
    }
}
