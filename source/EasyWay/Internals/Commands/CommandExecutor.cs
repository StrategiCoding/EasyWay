using EasyWay.Internals.Contexts;
using FluentValidation;
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

        public async Task<CommandResult> Execute<TCommand>(TCommand command, CancellationToken cancellationToken)
            where TCommand : Command<TModule>
        {
            _cancellationContextConstructor.Set(cancellationToken);

            var validator = _serviceProvider.GetService<IValidator<TCommand>>();

            if (validator is not null)
            {
                var result = validator.Validate(command);

                if (!result.IsValid)
                {
                    var errors = result.Errors
                    .GroupBy(x => x.PropertyName)
                    .ToDictionary(
                        g => g.Key,
                        g => g.Select(x => x.ErrorCode).ToArray()
                    );

                    return CommandResult.Validation(errors);
                }
            }

            var commandResult = await _serviceProvider
                .GetRequiredService<ICommandHandler<TModule, TCommand>>()
                .Handle(command);

            await _unitOfWorkCommandHandler.Handle();

            return commandResult;
        }

        public async Task<TCommandResult> Execute<TCommand, TCommandResult>(TCommand command, CancellationToken cancellationToken)
            where TCommand : Command<TModule, TCommandResult>
            where TCommandResult : OperationResult
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
