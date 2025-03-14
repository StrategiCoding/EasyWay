namespace EasyWay.Internals.DomainEvents
{
    internal sealed class DomainEventBulkPublisher : IDomainEventBulkPublisher
    {
        private readonly IDomainEventPublisher _domainEventPublisher;

        public DomainEventBulkPublisher(IDomainEventPublisher domainEventPublisher) 
        {
            _domainEventPublisher = domainEventPublisher;
        }

        public async Task Publish(IEnumerable<DomainEventContext> domainEventContexts) 
        {
            foreach (var domainEventContext in domainEventContexts) 
            {
                await _domainEventPublisher.Publish(domainEventContext).ConfigureAwait(false);
            }
        }
    }
}
