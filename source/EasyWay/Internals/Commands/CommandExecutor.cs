using EasyWay.Internals.Contexts;
using EasyWay.Internals.Validation;
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
            where TCommand : Command
        {
            _cancellationContextConstructor.Set(cancellationToken);

            var validator = _serviceProvider.GetService<IEasyWayValidator<TCommand>>();

            if (validator is not null)
            {
                var errors = validator.Validate(command);

                if (errors.Any())
                {
                    return CommandResult.Validation(errors);
                }
            }

            var commandResult = await _serviceProvider
                .GetRequiredService<ICommandHandler<TCommand>>()
                .Handle(command);

            await _unitOfWorkCommandHandler.Handle();

            return commandResult;
        }
    }
}
