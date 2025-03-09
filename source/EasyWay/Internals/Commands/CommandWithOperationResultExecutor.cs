using EasyWay.Internals.Contexts;
using EasyWay.Internals.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Commands
{
    internal sealed class CommandWithOperationResultExecutor<TModule> : ICommandWithOperationResultExecutor<TModule>
        where TModule : EasyWayModule
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly ICancellationContextConstructor _cancellationContextConstructor;

        private readonly IUnitOfWorkCommandHandler _unitOfWorkCommandHandler;

        public CommandWithOperationResultExecutor(
            IServiceProvider serviceProvider,
            ICancellationContextConstructor cancellationContextConstructor,
            IUnitOfWorkCommandHandler unitOfWorkCommandHandler)
        {
            _serviceProvider = serviceProvider;
            _cancellationContextConstructor = cancellationContextConstructor;
            _unitOfWorkCommandHandler = unitOfWorkCommandHandler;
        }

        public async Task<CommandResult<TOperationResult>> Command<TCommand, TOperationResult>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : Command<TOperationResult>
            where TOperationResult : OperationResult
        {
            _cancellationContextConstructor.Set(cancellationToken);

            var validator = _serviceProvider.GetService<IEasyWayValidator<TCommand>>();

            if (validator is not null)
            {
                var errors = validator.Validate(command);

                if (errors.Any())
                {
                    return CommandResult<TOperationResult>.Validation(errors);
                }
            }

            var commandHandler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand,TOperationResult>>();

            var commandResult = await commandHandler.Handle(command);

            await _unitOfWorkCommandHandler.Handle();

            return commandResult;
        }
    }
}
