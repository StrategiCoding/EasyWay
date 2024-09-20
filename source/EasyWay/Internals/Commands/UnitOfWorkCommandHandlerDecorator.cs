using EasyWay.Internals.UnitOfWorks;

namespace EasyWay.Internals.Commands
{
    internal sealed class UnitOfWorkCommandHandlerDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : Command
    {
        private readonly ICommandHandler<TCommand> _decoratedHandler;

        private readonly IUnitOfWork _unitOfWork;

        public UnitOfWorkCommandHandlerDecorator(
            ICommandHandler<TCommand> decoratedHandler,
            IUnitOfWork unitOfWork) 
        {
            _decoratedHandler = decoratedHandler;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(TCommand command)
        {
            await _decoratedHandler.Handle(command);

            await _unitOfWork.Commit();
        }
    }
}
