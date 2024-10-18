namespace EasyWay.Internals.Contexts
{
    internal interface ICancellationContextConstructor
    {
        void Set(CancellationToken token);
    }
}
