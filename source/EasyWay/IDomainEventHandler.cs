namespace EasyWay
{
    /// <summary>
    /// Defines a handler for an event
    /// </summary>
    /// <typeparam name="TDomainEvent">The type of event being handled</typeparam>
    public interface IDomainEventHandler<TDomainEvent>
        where TDomainEvent : DomainEvent
    {
        /// <summary>
        /// Handles an event
        /// </summary>
        /// <param name="domainEvent">Event</param>
        Task Handle(TDomainEvent domainEvent);
    }
}
