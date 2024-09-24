using EasyWay.Internals.AggregateRoots;
using Microsoft.EntityFrameworkCore;

namespace EasyWay.EntityFrameworkCore.Internals.AggregateRoots
{
    internal sealed class EntityFrameworkAggregateRootsContext : IAggregateRootsContext
    {
        private readonly DbContext _dbContext;

        public EntityFrameworkAggregateRootsContext(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IReadOnlyCollection<AggregateRoot> GetAggregateRoots()
        {
            var aggregateRoots = _dbContext
                .ChangeTracker
                .Entries<AggregateRoot>()
                .Select(x => x.Entity)
                .ToList();

            return aggregateRoots;
        }
    }
}
