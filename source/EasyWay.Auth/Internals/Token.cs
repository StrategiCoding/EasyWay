namespace EasyWay.Internals
{
    internal class Token
    {
        public string AccessToken { get; }

        internal Token(string accessToken)
        {
            AccessToken = accessToken;
        }
    }
}
