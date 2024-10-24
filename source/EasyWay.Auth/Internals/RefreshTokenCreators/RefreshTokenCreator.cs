using System.Security.Cryptography;

namespace EasyWay.Internals.RefreshTokenCreators
{
    internal sealed class RefreshTokenCreator : IRefreshTokenCreator
    {
        public string Create()
        {
            var randomNumber = new byte[32];

            var rng = RandomNumberGenerator.Create();

            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }
    }
}
