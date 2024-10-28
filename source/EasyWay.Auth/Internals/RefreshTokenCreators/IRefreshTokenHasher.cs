namespace EasyWay.Internals.RefreshTokenCreators
{
    internal interface IRefreshTokenHasher
    {
        byte[] Salt();

        string Hash(string refreshToken, byte[] salt);

        bool Verify(string refreshToken, byte[] salt, string hashedRefreshToken);
    }
}
