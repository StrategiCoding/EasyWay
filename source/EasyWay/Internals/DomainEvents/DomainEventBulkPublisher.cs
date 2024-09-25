namespace EasyWay.Internals.DomainEvents
{
    internal sealed class DomainEventBulkPublisher : IDomainEventBulkPublisher
    {
        private readonly IDomainEventPublisher _domainEventPublisher;

        public DomainEventBulkPublisher(IDomainEventPublisher domainEventPublisher) 
        {
            _domainEventPublisher = domainEventPublisher;
        }

        public async Task Publish<TDomainEvent>(IEnumerable<TDomainEvent> domainEvents) 
            where TDomainEvent : DomainEvent
        {
            foreach (var domainEvent in domainEvents) 
            {
                await _domainEventPublisher.Publish(domainEvent).ConfigureAwait(false);
            }
        }
    }
}
