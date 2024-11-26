using EasyWay.Internals.Contexts;
using FluentValidation;
using FluentValidation.Results;
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

        public async Task<CommandResult<TOperationResult>> Execute<TOperationResult>(Command<TModule, TOperationResult> command, CancellationToken cancellationToken)
            where TOperationResult : OperationResult
        {
            _cancellationContextConstructor.Set(cancellationToken);

            var commandType = command.GetType();

            var validatorType = typeof(IValidator<>).MakeGenericType(commandType);

            var validator = _serviceProvider.GetService(validatorType);

            if (validator is not null)
            {
                var result = (ValidationResult)validatorType
                    .GetMethod("Validate")
                    ?.Invoke(validator, [command]);

                if (!result.IsValid)
                {
                    var errors = result.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(x => x.ErrorCode).ToArray()
                    );

                    return CommandResult<TOperationResult>.Validation(errors);
                }
            }

            var commandHandlerType = typeof(ICommandHandler<,,>).MakeGenericType(typeof(TModule), commandType, typeof(TOperationResult));

            var commandHandler = _serviceProvider.GetRequiredService(commandHandlerType);

            var commandResult = await (Task<CommandResult<TOperationResult>>)commandHandlerType.GetMethod("Handle").Invoke(commandHandler, [command]);

            await _unitOfWorkCommandHandler.Handle();

            return commandResult;
        }
    }
}
