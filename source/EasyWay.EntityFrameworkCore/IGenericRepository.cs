namespace EasyWay.EntityFrameworkCore
{
    public interface IGenericRepository<TAggregateRoot>
        where TAggregateRoot : AggregateRoot
    {
        Task Add(TAggregateRoot aggregateRoot);

        Task<TAggregateRoot?> Get(Guid id);
    }
}
