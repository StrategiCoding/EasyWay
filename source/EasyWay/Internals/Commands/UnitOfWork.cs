using EasyWay.Internals.AggregateRoots;
using EasyWay.Internals.DomainEvents;

namespace EasyWay.Internals.Commands
{
    internal sealed class UnitOfWork
    {
        private readonly IDomainEventContextDispacher _domainEventDispacher;

        private readonly ConcurrencyTokenUpdater _concurrencyTokenUpdater;

        private readonly ITransaction _transaction;

        public UnitOfWork(
            IDomainEventContextDispacher domainEventDispacher,
            ConcurrencyTokenUpdater concurrencyTokenUpdater,
            ITransaction transaction)
        {
            _domainEventDispacher = domainEventDispacher;
            _concurrencyTokenUpdater = concurrencyTokenUpdater;
            _transaction = transaction;
        }

        public async Task Commit()
        {
            await _domainEventDispacher.Dispach().ConfigureAwait(false);

            _concurrencyTokenUpdater.Update();

            await _transaction.Commit().ConfigureAwait(false);
        }
    }
}
