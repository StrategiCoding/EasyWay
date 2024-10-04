using EasyWay.Internals.AggregateRoots;
using EasyWay.Internals.DomainEvents;
using EasyWay.Internals.UnitOfWorks;

namespace EasyWay.Internals.Commands
{
    internal sealed class UnitOfWorkCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : Command
    {
        private readonly ICommandHandler<TCommand> _decoratedHandler;

        private readonly IDomainEventContextDispacher _domainEventDispacher;

        private readonly IAggregateRootsContext _aggragateRootsContext;

        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkCommandHandlerDecorator(
            ICommandHandler<TCommand> decoratedHandler,
            IDomainEventContextDispacher domainEventDispacher,
            IAggregateRootsContext aggragateRootsContext,
            IUnitOfWork unitOfWork)
        {
            _decoratedHandler = decoratedHandler;
            _domainEventDispacher = domainEventDispacher;
            _aggragateRootsContext = aggragateRootsContext;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(TCommand command)
        {
            await _decoratedHandler.Handle(command).ConfigureAwait(false);

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
