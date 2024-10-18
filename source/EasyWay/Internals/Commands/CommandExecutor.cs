using EasyWay.Internals.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Commands
{
    internal sealed class CommandExecutor<TModule> : ICommandExecutor<TModule>
        where TModule : EasyWayModule
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly ICancellationContextConstructor _cancellationContextConstructor;

        private readonly UnitOfWorkCommandHandler _unitOfWorkCommandHandler;

        public CommandExecutor(
            IServiceProvider serviceProvider,
            ICancellationContextConstructor cancellationContextConstructor,
            UnitOfWorkCommandHandler unitOfWorkCommandHandler) 
        {
            _serviceProvider = serviceProvider;
            _cancellationContextConstructor = cancellationContextConstructor;
            _unitOfWorkCommandHandler = unitOfWorkCommandHandler;
        }

        public async Task Execute<TCommand>(TCommand command, CancellationToken cancellationToken)
            where TCommand : Command<TModule>
        {
            _cancellationContextConstructor.Set(cancellationToken);

             await _serviceProvider
                .GetRequiredService<ICommandHandler<TModule, TCommand>>()
                .Handle(command);

            await _unitOfWorkCommandHandler.Handle();
        }
    }
}
