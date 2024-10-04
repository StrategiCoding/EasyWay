using Microsoft.EntityFrameworkCore;

namespace EasyWay.Internals.AggregateRoots
{
    internal sealed class EntityFrameworkAggregateRootsContext : IAggregateRootsContext
    {
        private readonly DbContext _dbContext;

        public EntityFrameworkAggregateRootsContext(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IReadOnlyCollection<AggregateRoot> GetChangedAggregateRoots()
        {
            var aggregateRoots = _dbContext
                .ChangeTracker
                .Entries<AggregateRoot>()
                .Where(x => x.State != EntityState.Unchanged)
                .Select(x => x.Entity)
                .ToList();

            return aggregateRoots;
        }
    }
}
