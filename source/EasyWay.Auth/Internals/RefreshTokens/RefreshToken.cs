using System.Security.Cryptography;

namespace EasyWay.Internals.RefreshTokens
{
    internal sealed class RefreshToken : IRefreshToken
    {
        public string CreateRefreshToken()
        {
            var randomNumber = new byte[32];

            string result;

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);

                result = Convert.ToBase64String(randomNumber);
            }

            return result;
        }
    }
}
