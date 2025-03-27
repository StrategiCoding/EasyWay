using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EasyWay.Internals.Repositories
{
    internal sealed class WriteGenericRepository<TAggregateRoot> : IWriteGenericRepository<TAggregateRoot>
        where TAggregateRoot : AggregateRoot
    {
        private readonly DbSet<TAggregateRoot> _aggregateRoots;

        public WriteGenericRepository(DbContext dbContext)
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

        public Task Any()
        {
            return _aggregateRoots.AnyAsync();
        }

        public Task Any(Expression<Func<TAggregateRoot, bool>> predicate)
        {
            return _aggregateRoots.AnyAsync(predicate);
        }

        public Task<int> Count()
        {
            return _aggregateRoots.CountAsync();
        }

        public Task<int> Count(Expression<Func<TAggregateRoot, bool>> predicate)
        {
            return _aggregateRoots.CountAsync(predicate);
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
