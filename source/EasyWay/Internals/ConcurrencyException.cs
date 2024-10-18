namespace EasyWay.Internals
{
    internal sealed class ConcurrencyException : EasyWayException
    {
        internal ConcurrencyException(string message)
            : base(message) { }

        internal ConcurrencyException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
