namespace EasyWay.Internals.AggregateRoots
{
    internal interface IAggregateRootsContext
    {
        IReadOnlyCollection<AggregateRoot> GetAggregateRoots();
    }
}
