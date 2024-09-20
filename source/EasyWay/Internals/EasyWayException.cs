namespace EasyWay.Internals
{
    public abstract class EasyWayException : Exception
    {
        protected EasyWayException() { }

        protected EasyWayException(string message)
            : base (message) { }

        protected EasyWayException(string message, Exception innerException)
            : base (message, innerException) { }
    }
}
