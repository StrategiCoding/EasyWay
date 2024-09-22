using EasyWay.Internals.BusinessRules;
using EasyWay.Internals.DomainEvents;
using EasyWay.Internals.UnitOfWorks;

namespace EasyWay.Internals.Commands
{
    internal sealed class UnitOfWorkCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : Command
    {
        private readonly ICommandHandler<TCommand> _decoratedHandler;

        private readonly IDomainEventsContext _context;

        private readonly IDomainEventPublisher _publisher;

        private readonly IUnitOfWork _unitOfWork;

        private readonly IBrokenBusinessRuleDispacher _brokenBusinessRuleDispacher;

        public UnitOfWorkCommandHandlerDecorator(
            ICommandHandler<TCommand> decoratedHandler,
            IDomainEventsContext context,
            IDomainEventPublisher publisher,
            IUnitOfWork unitOfWork,
            IBrokenBusinessRuleDispacher brokenBusinessRuleDispacher) 
        {
            _decoratedHandler = decoratedHandler;
            _context = context;
            _publisher = publisher;
            _brokenBusinessRuleDispacher = brokenBusinessRuleDispacher;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(TCommand command)
        {
            try
            {
                await _decoratedHandler.Handle(command);

                var domainEvents = _context.GetAllDomainEvents();

                _context.ClearAllDomainEvents();

                foreach (var domainEvent in domainEvents)
                {
                    await _publisher.Publish(domainEvent).ConfigureAwait(false);
                }

                await _unitOfWork.Commit();
            }
            catch (BusinessRuleException businessRuleException)
            {
                await _brokenBusinessRuleDispacher.Dispach(businessRuleException.BrokenBusinessRule);

                var domainEvents = _context.GetAllDomainEvents();

                _context.ClearAllDomainEvents();

                foreach (var domainEvent in domainEvents)
                {
                    await _publisher.Publish(domainEvent).ConfigureAwait(false);
                }

                await _unitOfWork.Commit();

                throw;
            }
        }
    }
}
