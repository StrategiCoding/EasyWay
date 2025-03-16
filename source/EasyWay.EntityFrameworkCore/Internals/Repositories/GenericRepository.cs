using Microsoft.EntityFrameworkCore;

namespace EasyWay.Internals.Repositories
{
    internal sealed class GenericRepository<TAggregateRoot> : IGenericRepository<TAggregateRoot>
        where TAggregateRoot : AggregateRoot
    {
        private readonly DbSet<TAggregateRoot> _aggregateRoots;

        public GenericRepository(DbContext dbContext)
        {
            _aggregateRoots = dbContext.Set<TAggregateRoot>();
        }

        public Task Add(TAggregateRoot aggregateRoot)
        {
            return _aggregateRoots.AddAsync(aggregateRoot).AsTask();
        }

        public Task Add(IEnumerable<TAggregateRoot> aggregateRoots)
        {
            return _aggregateRoots.AddRangeAsync(aggregateRoots);
        }

        public Task<TAggregateRoot?> Get(Guid id)
        {
            return _aggregateRoots.FindAsync(id).AsTask();
        }

        public Task Remove(TAggregateRoot aggregateRoot)
        {
            _aggregateRoots.Remove(aggregateRoot);

            return Task.CompletedTask;
        }

        public Task Remove(IEnumerable<TAggregateRoot> aggregateRoots)
        {
            _aggregateRoots.RemoveRange(aggregateRoots);

            return Task.CompletedTask;
        }
    }
}
