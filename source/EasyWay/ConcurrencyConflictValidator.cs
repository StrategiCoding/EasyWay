using EasyWay.Internals;
using EasyWay.Internals.Commands.ConcurrencyConflict;

namespace EasyWay
{
    public sealed class ConcurrencyConflictValidator
    {
        internal ConcurrencyConflictValidator() { }

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

        public void Validate<T>(AggregateRoot aggregateRoot, Command<T> command)
            where T : OperationResult
        {
            var commandWithConcurrencyToken = command as IWithConcurrencyToken;
            
            if (commandWithConcurrencyToken is null)
            {
                throw new CommandWihtoutConcurrencyTokenException(command.GetType());
            }

            if (aggregateRoot.ConcurrencyToken == commandWithConcurrencyToken.ConcurrencyToken)
            {
                return;
            }

            var message = $"{aggregateRoot.GetType().Name} with id {aggregateRoot.Id} has different concurrency token ({aggregateRoot.ConcurrencyToken}) that command ({commandWithConcurrencyToken.ConcurrencyToken})";

            throw new ConcurrencyException(message);
        }
    }
}
