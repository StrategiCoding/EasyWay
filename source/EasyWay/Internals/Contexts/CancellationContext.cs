namespace EasyWay.Internals.Contexts
{
    internal sealed class CancellationContext : ICancellationContext
    {
        public CancellationToken Token { get; private set; }

        internal void Set(CancellationToken token) => Token = token;
    }
}
