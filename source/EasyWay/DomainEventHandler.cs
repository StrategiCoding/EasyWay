namespace EasyWay
{
    /// <summary>
    /// Defines a handler for an event
    /// </summary>
    /// <typeparam name="TDomainEvent">The type of event being handled</typeparam>
    public abstract class DomainEventHandler<TDomainEvent>
        where TDomainEvent : DomainEvent
    {
        /// <summary>
        /// Handles an event
        /// </summary>
        /// <param name="domainEvent">Event</param>
        public abstract Task Handle(TDomainEvent domainEvent);
    }
}
