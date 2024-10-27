using EasyWay.Internals.Domain.SeedWorks.Clocks;
using EasyWay.Settings;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EasyWay.Internals.AccessTokenCreators
{
    internal sealed class AccessTokensCreator : IAccessTokensCreator
    {
        private readonly IAuthSettings _settings;

        public AccessTokensCreator(IAuthSettings settings) 
        {
            _settings = settings;
        }

        public AccessToken Create(Guid userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_settings.SecretKey));

            var expires = SecurityClock.UtcNow.Add(_settings.AccessTokenLifetime);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
{
                    new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = expires,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
            };

            var accessToken = tokenHandler.CreateToken(tokenDescriptor);

            var stringToken = tokenHandler.WriteToken(accessToken);

            return new AccessToken(stringToken, expires);
        }
    }
}
