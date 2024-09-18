namespace EasyWay.Internals
{
    internal abstract class EasyWayException : Exception
    {
        protected EasyWayException() { }

        protected EasyWayException(string message)
            : base (message) { }
    }
}
