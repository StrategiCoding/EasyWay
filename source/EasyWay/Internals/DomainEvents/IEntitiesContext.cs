namespace EasyWay.Internals.DomainEvents
{
    internal interface IEntitiesContext
    {
        IEnumerable<AggregateRoot> GetAggregateRoots();

        IEnumerable<Entity> GetEntities();
    }
}
