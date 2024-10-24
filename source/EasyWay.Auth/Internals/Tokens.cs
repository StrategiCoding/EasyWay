namespace EasyWay.Internals
{
    internal sealed class Tokens
    {
        public string RefreshToken { get; }

        public DateTime RefreshTokenExpires { get; }

        public string AccessToken { get; }

        internal Tokens(string refreshToken, DateTime refreshTokenExpires, string accessToken)
        {
            RefreshToken = refreshToken;
            RefreshTokenExpires = refreshTokenExpires;
            AccessToken = accessToken;
        }
    }
}
