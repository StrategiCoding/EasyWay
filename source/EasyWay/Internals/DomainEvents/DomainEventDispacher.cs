namespace EasyWay.Internals.DomainEvents
{
    internal sealed class DomainEventDispacher : IDomainEventDispacher
    {
        private readonly IDomainEventBulkPublisher _publisher;

        private readonly IDomainEventsContext _context;

        public DomainEventDispacher(
            IDomainEventBulkPublisher publisher,
            IDomainEventsContext context) 
        {
            _publisher = publisher;
            _context = context;
        }

        public async Task Dispach()
        {
            var domainEvents = _context.GetAllDomainEvents();

            _context.ClearAllDomainEvents();

            await _publisher
                .Publish(domainEvents)
                .ConfigureAwait(false);

            domainEvents = _context.GetAllDomainEvents();

            while (domainEvents.Any()) 
            {
                _context.ClearAllDomainEvents();

                await _publisher
                    .Publish(domainEvents)
                    .ConfigureAwait(false);

                domainEvents = _context.GetAllDomainEvents();
            }
        }
    }
}