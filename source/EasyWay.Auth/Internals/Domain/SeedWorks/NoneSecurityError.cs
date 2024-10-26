namespace EasyWay.Internals.Domain.SeedWorks
{
    internal sealed class NoneSecurityError : SecurityError
    {
        internal override string Message { get; } = "None";
    }
}
