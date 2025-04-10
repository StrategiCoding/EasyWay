namespace EasyWay.Internals.DomainEvents
{
    internal sealed class DomainEventsAccessor : IDomainEventsAccessor
    {
        private readonly IEntitiesContext _context;

        public DomainEventsAccessor(IEntitiesContext context) => _context = context;

        public IReadOnlyCollection<DomainEventContext> GetAllDomainEvents()
        {
            var domainEntities = _context
                .GetEntities()
                .Where(x => x.DomainEvents != null && x.DomainEvents.Any())
                .ToList();


            return domainEntities
                .SelectMany(x => x.DomainEvents)
                .ToList();
        }

        public void ClearAllDomainEvents()
        {
            var domainEntities = _context
                .GetEntities()
                .Where(x => x.DomainEvents != null && x.DomainEvents.Any()).ToList();

            domainEntities
                .ForEach(entity => entity.ClearDomainEvents());
        }
    }
}
