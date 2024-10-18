using EasyWay.Internals.AggregateRoots;
using EasyWay.Internals.DomainEvents;
using EasyWay.Internals.Transactions;

namespace EasyWay.Internals.Commands
{
    internal sealed class UnitOfWorkCommandHandler : IUnitOfWorkCommandHandler
    {
        private readonly IDomainEventContextDispacher _domainEventDispacher;

        private readonly IAggregateRootsContext _aggragateRootsContext;

        private readonly ITransaction _unitOfWork;

        public UnitOfWorkCommandHandler(
            IDomainEventContextDispacher domainEventDispacher,
            IAggregateRootsContext aggragateRootsContext,
            ITransaction unitOfWork)
        {
            _domainEventDispacher = domainEventDispacher;
            _aggragateRootsContext = aggragateRootsContext;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle()
        {
            await _domainEventDispacher.Dispach().ConfigureAwait(false);

            var aggragateRoots = _aggragateRootsContext.GetAggregateRoots();

            foreach (var aggragateRoot in aggragateRoots)
            {
                aggragateRoot.UpdateConcurrencyToken();
            }

            await _unitOfWork.Commit().ConfigureAwait(false);
        }
    }
}
