namespace EasyWay.Internals.DomainEvents
{
    internal interface IDomainEventBulkPublisher
    {
        Task Publish<TDomainEvent>(IEnumerable<TDomainEvent> domainEvents)
            where TDomainEvent : DomainEvent;
    }
}
