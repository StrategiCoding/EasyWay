namespace EasyWay.Internals.DomainEvents
{
    internal sealed class DomainEventContextDispacher : IDomainEventContextDispacher
    {
        private readonly IDomainEventBulkPublisher _publisher;

        private readonly IDomainEventsContext _context;

        public DomainEventContextDispacher(
            IDomainEventBulkPublisher publisher,
            IDomainEventsContext context) 
        {
            _publisher = publisher;
            _context = context;
        }

        public async Task Dispach()
        {
            var domainEvents = _context.GetAllDomainEvents();

            if (!domainEvents.Any()) 
            {
                return;
            }

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