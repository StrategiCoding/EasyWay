using EasyWay.Internals.RefreshTokens;
using EasyWay.Internals.Storage;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EasyWay.Internals
{
    internal sealed class TokensCreator : ITokensCreator
    {
        private readonly string SecretKey = "XN32ifS0ZumZ0QZTAFyY86GdQRPnTHjwzh42KpflDemEZ+Ewlzpgb3N5l8u9/jWV";

        private TimeSpan TokenLifeTime = TimeSpan.FromMinutes(10);

        private readonly IRefreshToken _refreshToken;

        private readonly ITokenStorage _tokenStorage;

        public TokensCreator(
            IRefreshToken refreshToken,
            ITokenStorage tokenStorage) 
        { 
            _refreshToken = refreshToken;
            _tokenStorage = tokenStorage;
        }

        public Tokens Create(Guid userId, string role)
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
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                }),
                Expires = expires,
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature),
            };

            var jwtToken = tokenHandler.CreateToken(tokenDescriptor);

            var accessToken = tokenHandler.WriteToken(jwtToken);

            var refreshToken = _refreshToken.CreateRefreshToken();

            var token = new Tokens(accessToken, refreshToken);

            return token;
        }
    }
}
