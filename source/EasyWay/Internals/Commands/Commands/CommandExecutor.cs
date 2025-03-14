using EasyWay.Internals.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Commands.Commands
{
    internal sealed class CommandExecutor : ICommandExecutor
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly CancellationContext _cancellationContext;

        private readonly UnitOfWork _unitOfWork;

        public CommandExecutor(
            IServiceProvider serviceProvider,
            CancellationContext cancellationContext,
            UnitOfWork unitOfWork)
        {
            _serviceProvider = serviceProvider;
            _cancellationContext = cancellationContext;
            _unitOfWork = unitOfWork;
        }

        public async Task<CommandResult> Execute<TModule, TCommand>(TCommand command, CancellationToken cancellationToken)
            where TModule : EasyWayModule
            where TCommand : Command
        {
            _cancellationContext.Set(cancellationToken);

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
                .GetRequiredService<CommandHandler<TCommand>>()
                .Handle(command);

            await _unitOfWork.Commit();

            return commandResult;
        }
    }
}
