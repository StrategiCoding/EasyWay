namespace EasyWay.Internals.DomainEvents
{
    internal sealed class DomainEventCannotBeNullException<TDomainEvent> : EasyWayException
        where TDomainEvent : DomainEvent
    {
        internal DomainEventCannotBeNullException()
            : base($"{typeof(TDomainEvent).Name} cannot be null") { }
    }
}
