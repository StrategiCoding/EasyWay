namespace EasyWay.Internals.UnitOfWorks
{
    internal sealed class ConcurrencyException : Exception
    {
        public ConcurrencyException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
