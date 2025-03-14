namespace EasyWay.Internals.DomainEvents
{
    internal interface IDomainEventPublisher
    {
        Task Publish(DomainEventContext domainEventContext);
    }
}
