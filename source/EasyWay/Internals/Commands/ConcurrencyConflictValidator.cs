using EasyWay.Internals.UnitOfWorks;

namespace EasyWay.Internals.Commands
{
    internal sealed class ConcurrencyConflictValidator : IConcurrencyConflictValidator
    {
        public void Validate<TAggregateRoot>(
            TAggregateRoot aggregateRoot,
            IWithConcurrencyToken command)
            where TAggregateRoot : AggregateRoot
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
