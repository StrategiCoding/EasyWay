using System.Linq.Expressions;

namespace EasyWay
{
    public interface IWriteGenericRepository<TAggregateRoot>
        where TAggregateRoot : AggregateRoot
    {
        Task Add(TAggregateRoot aggregateRoot);

        Task Add(IEnumerable<TAggregateRoot> aggregateRoots);

        Task<TAggregateRoot?> Get(Guid id);

        Task<IEnumerable<TAggregateRoot>> Get(Expression<Func<TAggregateRoot, bool>> predicate);

        Task Remove(TAggregateRoot aggregateRoot);

        Task Remove(IEnumerable<TAggregateRoot> aggregateRoots);

        Task Any();

        Task Any(Expression<Func<TAggregateRoot, bool>> predicate);

        Task<int> Count();

        Task<int> Count(Expression<Func<TAggregateRoot, bool>> predicate);
    }
}
