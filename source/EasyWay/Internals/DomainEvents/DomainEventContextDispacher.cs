namespace EasyWay.Internals.DomainEvents
{
    internal sealed class DomainEventContextDispacher : IDomainEventContextDispacher
    {
        private readonly IDomainEventBulkPublisher _publisher;

        private readonly IDomainEventsAccessor _context;

        public DomainEventContextDispacher(
            IDomainEventBulkPublisher publisher,
            IDomainEventsAccessor context) 
        {
            _publisher = publisher;
            _context = context;
        }

        public async Task Dispach()
        {
            var domainEventContexts = _context.GetAllDomainEvents();

            if (!domainEventContexts.Any()) 
            {
                return;
            }

            _context.ClearAllDomainEvents();

            await _publisher
                .Publish(domainEventContexts)
                .ConfigureAwait(false);

            domainEventContexts = _context.GetAllDomainEvents();

            while (domainEventContexts.Any()) 
            {
                _context.ClearAllDomainEvents();

                await _publisher
                    .Publish(domainEventContexts)
                    .ConfigureAwait(false);

                domainEventContexts = _context.GetAllDomainEvents();
            }
        }
    }
}