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

        public async Task<CommandResult<TOperationResult>> Execute<TOperationResult>(Command<TOperationResult> command, CancellationToken cancellationToken)
            where TOperationResult : OperationResult
        {
            _cancellationContextConstructor.Set(cancellationToken);

            var commandType = command.GetType();

            var validatorType = typeof(IEasyWayValidator<>).MakeGenericType(commandType);

            var validator = _serviceProvider.GetService(validatorType);

            if (validator is not null)
            {
                var errors = (IDictionary<string, string[]>)validatorType
                    .GetMethod("Validate")
                    ?.Invoke(validator, [command]);

                if (errors.Any())
                {
                    return CommandResult<TOperationResult>.Validation(errors);
                }
            }

            var commandHandlerType = typeof(ICommandHandler<,>).MakeGenericType(commandType, typeof(TOperationResult));

            var commandHandler = _serviceProvider.GetRequiredService(commandHandlerType);

            var commandResult = await (Task<CommandResult<TOperationResult>>)commandHandlerType.GetMethod("Handle").Invoke(commandHandler, [command]);

            await _unitOfWorkCommandHandler.Handle();

            return commandResult;
        }
    }
}
