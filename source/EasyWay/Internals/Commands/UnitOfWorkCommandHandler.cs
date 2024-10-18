using EasyWay.Internals.AggregateRoots;
using EasyWay.Internals.DomainEvents;
using EasyWay.Internals.Transactions;

namespace EasyWay.Internals.Commands
{
    internal sealed class UnitOfWorkCommandHandler : IUnitOfWorkCommandHandler
    {
        private readonly IDomainEventContextDispacher _domainEventDispacher;

        private readonly IConcurrencyTokenUpdater _concurrencyTokenUpdater;

        private readonly ITransaction _unitOfWork;

        public UnitOfWorkCommandHandler(
            IDomainEventContextDispacher domainEventDispacher,
            IConcurrencyTokenUpdater concurrencyTokenUpdater,
            ITransaction unitOfWork)
        {
            _domainEventDispacher = domainEventDispacher;
            _concurrencyTokenUpdater = concurrencyTokenUpdater;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle()
        {
            await _domainEventDispacher.Dispach().ConfigureAwait(false);

            _concurrencyTokenUpdater.Update();

            await _unitOfWork.Commit().ConfigureAwait(false);
        }
    }
}
