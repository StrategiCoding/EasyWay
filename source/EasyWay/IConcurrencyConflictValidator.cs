namespace EasyWay
{
    public interface IConcurrencyConflictValidator
    {
        void Validate<TAggregateRoot>(TAggregateRoot aggregateRoot, IWithConcurrencyToken token)
            where TAggregateRoot : AggregateRoot;
    }
}
