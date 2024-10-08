﻿namespace EasyWay.Internals.UnitOfWorks
{
    internal sealed class ConcurrencyException : EasyWayException
    {
        internal ConcurrencyException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
