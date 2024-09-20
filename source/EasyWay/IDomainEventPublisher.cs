namespace EasyWay
{
    public interface IDomainEventPublisher
    {
        Task Publish<TEvent>(TEvent @event)
            where TEvent : DomainEvent;
    }
}
