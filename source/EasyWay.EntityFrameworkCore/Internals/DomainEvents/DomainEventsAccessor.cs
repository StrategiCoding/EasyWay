using EasyWay.Internals.DomainEvents;
using Microsoft.EntityFrameworkCore;

namespace EasyWay.EntityFrameworkCore.Internals.DomainEvents
{
    internal sealed class DomainEventsAccessor : IDomainEventsAccessor
    {
        private readonly DbContext _dbContext;

        public DomainEventsAccessor(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IReadOnlyCollection<DomainEvent> GetAllDomainEvents()
        {
            var domainEntities = _dbContext.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            return domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();
        }

        public void ClearAllDomainEvents()
        {
            var domainEntities = _dbContext.ChangeTracker
                .Entries<Entity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any()).ToList();

            domainEntities
                .ForEach(entity => entity.Entity.ClearDomainEvents());
        }
    }
}
