namespace EasyWay
{
    public interface IEventPublisher
    {
        Task Publish<TEvent>(TEvent @event)
            where TEvent : DomainEvent;
    }
}
