namespace EasyWay.Internals.Contexts
{
    internal sealed class DefaultCorrelationContext : ICorrelationContext
    {
        public DefaultCorrelationContext() 
        {
            CorrelationId = Guid.NewGuid();
        }

        public Guid CorrelationId { get; }
    }
}
