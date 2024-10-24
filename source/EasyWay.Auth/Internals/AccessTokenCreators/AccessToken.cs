namespace EasyWay.Internals.AccessTokenCreators
{
    internal sealed class AccessToken
    {
        internal string Token { get; }

        internal DateTime Expires { get; }

        internal AccessToken(
            string token,
            DateTime expires)
        {
            Token = token;
            Expires = expires;
        }
    }
}
