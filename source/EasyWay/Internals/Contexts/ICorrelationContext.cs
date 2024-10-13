namespace EasyWay.Internals.Contexts
{
    internal interface ICorrelationContext
    {
        Guid CorrelationId { get; }
    }
}
