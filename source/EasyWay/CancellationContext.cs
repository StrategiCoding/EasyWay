namespace EasyWay
{
    public sealed class CancellationContext
    {
        public CancellationToken Token { get; private set; }

        internal void Set(CancellationToken token) => Token = token;
    }
}
