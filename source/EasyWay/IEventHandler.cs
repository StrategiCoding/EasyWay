namespace EasyWay
{
    /// <summary>
    /// Defines a handler for an event
    /// </summary>
    /// <typeparam name="TEvent">The type of event being handled</typeparam>
    public interface IEventHandler<TEvent>
        where TEvent : Event
    {
        /// <summary>
        /// Handles an event
        /// </summary>
        /// <param name="event">Event</param>
        Task Handle(TEvent @event);
    }
}
