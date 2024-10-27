namespace EasyWay.Internals.Domain.SeedWorks.Results
{
    internal abstract class SecurityError
    {
        internal string Code => GetType().Name;

        internal SecurityError() { }
    }
}
