using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EasyWay.Internals
{
    internal sealed class JwtFactory : IJwtFactory
    {
        private readonly string SecretKey = "XN32ifS0ZumZ0QZTAFyY86GdQRPnTHjwzh42KpflDemEZ+Ewlzpgb3N5l8u9/jWV";

        private TimeSpan TokenLifeTime = TimeSpan.FromMinutes(10);

        public string Create(Guid userId, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

            var expires = Clock.UtcNow.Add(TokenLifeTime);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
    {
                    new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, Guid.NewGuid().ToString()),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = expires,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
                // TODO JWE
                // TODO RefreshToken
                // TODO AsymetricKeys (verify JWT based on signature)
            };

            var jwtToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(jwtToken);
        }
    }
}
