namespace EasyWay.Internals.Contexts
{
    internal sealed class CancellationContext : ICancellationContext, ICancellationContextConstructor
    {
        public CancellationToken Token { get; private set; }

        public void Set(CancellationToken token) => Token = token;
    }
}
