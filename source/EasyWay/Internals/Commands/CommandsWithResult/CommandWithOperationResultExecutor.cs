using EasyWay.Internals.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Commands.CommandsWithResult
{
    internal sealed class CommandWithOperationResultExecutor : ICommandWithOperationResultExecutor
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly CancellationContext _cancellationContext;

        private readonly UnitOfWork _unitOfWork;

        public CommandWithOperationResultExecutor(
            IServiceProvider serviceProvider,
            CancellationContext cancellationContext,
            UnitOfWork unitOfWork)
        {
            _serviceProvider = serviceProvider;
            _cancellationContext = cancellationContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult<TOperationResult>> Command<TModule, TCommand, TOperationResult>(TCommand command, CancellationToken cancellationToken = default)
            where TModule : EasyWayModule
            where TCommand : Command<TOperationResult>
            where TOperationResult : OperationResult
        {
            _cancellationContext.Set(cancellationToken);

            var validator = _serviceProvider.GetService<IEasyWayValidator<TCommand>>();

            if (validator is not null)
            {
                var errors = validator.Validate(command);

                if (errors.Any())
                {
                    return CommandResult<TOperationResult>.Validation(errors);
                }
            }

            var commandHandler = _serviceProvider.GetRequiredService<CommandHandler<TCommand,TOperationResult>>();

            var commandResult = await commandHandler.Handle(command);

            await _unitOfWork.Commit();

            return commandResult;
        }
    }
}
