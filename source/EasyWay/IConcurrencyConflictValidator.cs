namespace EasyWay
{
    public interface IConcurrencyConflictValidator
    {
        void Validate<TCommand>(AggregateRoot aggregateRoot, TCommand command)
            where TCommand : Command, IWithConcurrencyToken;
    }
}
