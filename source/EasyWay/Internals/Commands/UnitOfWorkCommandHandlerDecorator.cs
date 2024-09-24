using EasyWay.Internals.AggregateRoots;
using EasyWay.Internals.DomainEvents;
using EasyWay.Internals.UnitOfWorks;

namespace EasyWay.Internals.Commands
{
    internal sealed class UnitOfWorkCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : Command
    {
        private readonly ICommandHandler<TCommand> _decoratedHandler;

        private readonly IDomainEventsContext _domainEventsContext;

        private readonly IAggregateRootsContext _aggragateRootsContext;

        private readonly IDomainEventPublisher _publisher;

        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkCommandHandlerDecorator(
            ICommandHandler<TCommand> decoratedHandler,
            IDomainEventsContext domainEventsContext,
            IAggregateRootsContext aggragateRootsContext,
            IDomainEventPublisher publisher,
            IUnitOfWork unitOfWork)
        {
            _decoratedHandler = decoratedHandler;
            _domainEventsContext = domainEventsContext;
            _aggragateRootsContext = aggragateRootsContext;
            _publisher = publisher;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(TCommand command)
        {
            await _decoratedHandler.Handle(command);

            var domainEvents = _domainEventsContext.GetAllDomainEvents();

            _domainEventsContext.ClearAllDomainEvents();

            foreach (var domainEvent in domainEvents)
            {
                await _publisher.Publish(domainEvent).ConfigureAwait(false);
            }

            var aggragateRoots = _aggragateRootsContext.GetAggregateRoots();

            foreach (var aggragateRoot in aggragateRoots)
            {
                aggragateRoot.ConcurrencyToken++;
            }

            await _unitOfWork.Commit();
        }
    }
}
