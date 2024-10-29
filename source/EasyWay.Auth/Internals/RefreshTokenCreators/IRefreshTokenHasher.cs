namespace EasyWay.Internals.RefreshTokenCreators
{
    internal interface IRefreshTokenHasher
    {
        string Hash(string refreshToken);

        bool Verify(string refreshToken, string hashedRefreshToken);
    }
}
