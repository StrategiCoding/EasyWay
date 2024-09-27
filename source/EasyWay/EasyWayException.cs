namespace EasyWay
{
    public abstract class EasyWayException : Exception
    {
        internal EasyWayException() { }

        internal EasyWayException(string message)
            : base(message) { }

        internal EasyWayException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
