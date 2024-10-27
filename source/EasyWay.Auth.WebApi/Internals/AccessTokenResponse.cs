namespace EasyWay.Internals
{
    internal sealed class AccessTokenResponse
    {
        public string AccessToken { get; }

        internal AccessTokenResponse(string accessToken)
        {
            AccessToken = accessToken;
        }
    }
}
