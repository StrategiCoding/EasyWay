namespace EasyWay.Internals.DomainEvents
{
    internal interface IDomainEventPublisher
    {
        Task Publish<TEvent>(TEvent @event)
            where TEvent : DomainEvent;
    }
}
