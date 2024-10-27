using EasyWay.Settings;
using System.Security.Cryptography;

namespace EasyWay.Internals.RefreshTokenCreators
{
    internal sealed class RefreshTokenCreator : IRefreshTokenCreator
    {
        private readonly IAuthSettings _settings;

        public RefreshTokenCreator(IAuthSettings settings)
        {
            _settings = settings;
        }

        public string Create()
        {
            var randomNumber = new byte[_settings.RefreshTokenSize];

            var rng = RandomNumberGenerator.Create();

            rng.GetBytes(randomNumber);

            return Convert.ToBase64String(randomNumber);
        }
    }
}
