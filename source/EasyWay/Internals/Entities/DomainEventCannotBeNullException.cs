namespace EasyWay.Internals.Entities
{
    internal sealed class DomainEventCannotBeNullException<TDomainEvent> : EasyWayException
        where TDomainEvent : DomainEvent
    {
        internal DomainEventCannotBeNullException()
            : base($"{typeof(TDomainEvent).Name} cannot be null") { }
    }
}
