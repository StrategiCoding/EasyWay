using EasyWay.Internals.DomainEvents;
using Microsoft.EntityFrameworkCore;

namespace EasyWay.Internals.Entities
{
    internal sealed class EntityFrameworkEntitiesContext : IEntitiesContext
    {
        private readonly DbContext _dbContext;

        public EntityFrameworkEntitiesContext(DbContext dbContext) => _dbContext = dbContext;

        public IEnumerable<AggregateRoot> GetAggregateRoots() => _dbContext.ChangeTracker.Entries<AggregateRoot>().Select(x => x.Entity);

        public IEnumerable<Entity> GetEntities() => _dbContext.ChangeTracker.Entries<Entity>().Select(x => x.Entity);
    }
}
