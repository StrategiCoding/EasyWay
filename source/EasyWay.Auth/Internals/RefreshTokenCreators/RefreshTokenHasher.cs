using System.Security.Cryptography;

namespace EasyWay.Internals.RefreshTokenCreators
{
    internal sealed class RefreshTokenHasher : IRefreshTokenHasher
    {
        const int KeySize = 256 / 8;

        const int Iterations = 3;

        private readonly HashAlgorithmName AlgorithmName = HashAlgorithmName.SHA256;

        public byte[] Salt() => RandomNumberGenerator.GetBytes(KeySize);

        public string Hash(string refreshToken, byte[] salt)
        {
            var key = Rfc2898DeriveBytes.Pbkdf2(refreshToken, salt, Iterations, AlgorithmName, KeySize);

            return Convert.ToBase64String(key);
        }

        public bool Verify(string refreshToken, byte[] salt, string hashedRefreshToken)
        {
            return Convert.FromBase64String(hashedRefreshToken) == Rfc2898DeriveBytes.Pbkdf2(refreshToken, salt, Iterations, AlgorithmName, KeySize);
        }
    }
}
