namespace EasyWay.Internals.Commands
{
    internal sealed class ConcurrencyConflictValidator : IConcurrencyConflictValidator
    {
        public void Validate<TCommand>(AggregateRoot aggregateRoot, TCommand command) 
            where TCommand : Command, IWithConcurrencyToken
        {
            if (aggregateRoot.ConcurrencyToken == command.ConcurrencyToken)
            {
                return;
            }

            var message = $"{aggregateRoot.GetType().Name} with id {aggregateRoot.Id} has different concurrency token ({aggregateRoot.ConcurrencyToken}) that command ({command.ConcurrencyToken})";

            throw new ConcurrencyException(message);
        }
    }
}
