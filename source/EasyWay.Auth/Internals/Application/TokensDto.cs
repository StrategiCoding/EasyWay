namespace EasyWay.Internals.Application
{
    internal sealed class TokensDto
    {
        public string RefreshToken { get; }

        public DateTime RefreshTokenExpires { get; }

        public string AccessToken { get; }

        internal TokensDto(string refreshToken, DateTime refreshTokenExpires, string accessToken)
        {
            RefreshToken = refreshToken;
            RefreshTokenExpires = refreshTokenExpires;
            AccessToken = accessToken;
        }
    }
}
