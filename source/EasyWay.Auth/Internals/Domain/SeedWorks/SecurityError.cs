namespace EasyWay.Internals.Domain.SeedWorks
{
    internal abstract class SecurityError
    {
        internal string Code => GetType().Name;

        internal abstract string Message { get; }

        internal SecurityError() { }
    }
}
