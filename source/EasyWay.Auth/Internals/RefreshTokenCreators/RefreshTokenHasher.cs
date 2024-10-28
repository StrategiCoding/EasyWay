using EasyWay.Settings;
using System.Security.Cryptography;

namespace EasyWay.Internals.RefreshTokenCreators
{
    internal sealed class RefreshTokenHasher : IRefreshTokenHasher
    {
        const int KeySize = 256 / 8;

        const int Iterations = 3;

        private readonly HashAlgorithmName AlgorithmName = HashAlgorithmName.SHA256;

        private readonly byte[] _salt;

        public RefreshTokenHasher(IAuthSettings settings)
        {
            _salt = Convert.FromBase64String(settings.RefreshTokenSalt);
        }

        public string Hash(string refreshToken)
        {
            var key = Rfc2898DeriveBytes.Pbkdf2(refreshToken,_salt, Iterations, AlgorithmName, KeySize);

            return Convert.ToBase64String(key);
        }

        public bool Verify(string refreshToken, string hashedRefreshToken)
        {
            return Convert.FromBase64String(hashedRefreshToken) == Rfc2898DeriveBytes.Pbkdf2(refreshToken, _salt, Iterations, AlgorithmName, KeySize);
        }
    }
}
