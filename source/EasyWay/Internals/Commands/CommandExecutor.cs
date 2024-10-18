using EasyWay.Internals.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Commands
{
    internal sealed class CommandExecutor<TModule> : ICommandExecutor<TModule>
        where TModule : EasyWayModule
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly CancellationContext _cancellationContext;

        private readonly UnitOfWorkCommandHandler _unitOfWorkCommandHandler;

        public CommandExecutor(
            IServiceProvider serviceProvider,
            CancellationContext cancellationContext,
            UnitOfWorkCommandHandler unitOfWorkCommandHandler) 
        {
            _serviceProvider = serviceProvider;
            _cancellationContext = cancellationContext;
            _unitOfWorkCommandHandler = unitOfWorkCommandHandler;
        }

        public async Task Execute<TCommand>(TCommand command, CancellationToken cancellationToken)
            where TCommand : Command<TModule>
        {
            _cancellationContext.Set(cancellationToken);

             await _serviceProvider
                .GetRequiredService<ICommandHandler<TModule, TCommand>>()
                .Handle(command);

            await _unitOfWorkCommandHandler.Handle();
        }
    }
}
