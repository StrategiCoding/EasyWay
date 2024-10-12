namespace EasyWay
{
    public interface IConcurrencyConflictValidator
    {
        void Validate<TAggregateRoot>(TAggregateRoot aggregateRoot, IWithConcurrencyToken command)
            where TAggregateRoot : AggregateRoot;
    }
}
