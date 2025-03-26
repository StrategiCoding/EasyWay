namespace EasyWay
{
    public interface IWriteGenericRepository<TAggregateRoot>
        where TAggregateRoot : AggregateRoot
    {
        Task Add(TAggregateRoot aggregateRoot);

        Task Add(IEnumerable<TAggregateRoot> aggregateRoots);

        Task<TAggregateRoot?> Get(Guid id);

        Task Remove(TAggregateRoot aggregateRoot);

        Task Remove(IEnumerable<TAggregateRoot> aggregateRoots);
    }
}
