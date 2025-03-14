namespace EasyWay.Internals.DomainEvents
{
    internal interface IDomainEventBulkPublisher
    {
        Task Publish(IEnumerable<DomainEventContext> domainEventContexts);
    }
}
