namespace EasyWay
{
    public sealed class Tokens
    {
        public string AccessToken { get; }

        public string RefreshToken { get; }

        internal Tokens(
            string accessToken,
            string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }

    }
}
