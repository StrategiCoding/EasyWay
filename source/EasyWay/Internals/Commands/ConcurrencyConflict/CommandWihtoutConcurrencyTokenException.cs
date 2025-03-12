namespace EasyWay.Internals.Commands.ConcurrencyConflict
{
    internal sealed class CommandWihtoutConcurrencyTokenException : EasyWayException
    {
        internal CommandWihtoutConcurrencyTokenException(Type commandType)
        : base($"Add {nameof(IWithConcurrencyToken)} interface to {commandType.Name}"){ }
    }
}
