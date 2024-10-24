using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EasyWay.Internals.AccessTokenCreators
{
    internal sealed class AccessTokensCreator : IAccessTokensCreator
    {
        //TODO appsettings
        private readonly string SecretKey = "XN32ifS0ZumZ0QZTAFyY86GdQRPnTHjwzh42KpflDemEZ+Ewlzpgb3N5l8u9/jWV";

        private TimeSpan TokenLifeTime = TimeSpan.FromMinutes(10);

        public AccessToken Create(Guid userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

            var expires = DateTime.UtcNow.Add(TokenLifeTime);

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
