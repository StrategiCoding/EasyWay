namespace EasyWay.Internals.CancellationTokens
{
    internal sealed class CancellationTokenProvider : ICancellationTokenProvider
    {
        public CancellationToken Token { get; private set; }

        internal void Set(CancellationToken token) => Token = token;
    }
}
