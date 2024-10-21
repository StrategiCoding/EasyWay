using EasyWay.Internals.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Commands
{
    internal sealed class CommandExecutor<TModule> : ICommandExecutor<TModule>
        where TModule : EasyWayModule
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly ICancellationContextConstructor _cancellationContextConstructor;

        private readonly IUnitOfWorkCommandHandler _unitOfWorkCommandHandler;

        public CommandExecutor(
            IServiceProvider serviceProvider,
            ICancellationContextConstructor cancellationContextConstructor,
            IUnitOfWorkCommandHandler unitOfWorkCommandHandler) 
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

        public async Task<TCommandResult> Execute<TCommand, TCommandResult>(TCommand command, CancellationToken cancellationToken)
            where TCommand : Command<TModule, TCommandResult>
            where TCommandResult : CommandResult
        {
            _cancellationContextConstructor.Set(cancellationToken);

            var result = await _serviceProvider
               .GetRequiredService<ICommandHandler<TModule, TCommand, TCommandResult>>()
               .Handle(command);

            await _unitOfWorkCommandHandler.Handle();

            return result;
        }
    }
}
