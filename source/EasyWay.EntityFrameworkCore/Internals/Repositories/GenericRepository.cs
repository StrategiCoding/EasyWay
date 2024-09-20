using Microsoft.EntityFrameworkCore;

namespace EasyWay.EntityFrameworkCore.Internals.Repositories
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

        public Task<TAggregateRoot?> Get(Guid id)
        {
            return _aggregateRoots.FindAsync(id).AsTask();
        }
    }
}
