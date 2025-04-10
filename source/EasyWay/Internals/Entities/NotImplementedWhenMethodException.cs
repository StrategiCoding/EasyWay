namespace EasyWay.Internals.Entities
{
    internal sealed class NotImplementedWhenMethodException<TDomainEvent> : EasyWayException
        where TDomainEvent : DomainEvent
    {
        internal NotImplementedWhenMethodException()
            : base("Not implemented 'When' method for " + typeof(TDomainEvent).Name) { }
    }
}
