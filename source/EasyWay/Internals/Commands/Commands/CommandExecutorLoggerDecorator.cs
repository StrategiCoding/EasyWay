using EasyWay.Internals.Queries.Loggers;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Commands.Commands
{
    internal sealed class CommandExecutorLoggerDecorator : ICommandExecutor
    {
        private readonly ICommandExecutor _decoratedCommandExecutor;

        private readonly IServiceProvider _serviceProvider;

        public CommandExecutorLoggerDecorator(
            ICommandExecutor decoratedCommandExecutor,
            IServiceProvider serviceProvider)
        {
            _decoratedCommandExecutor = decoratedCommandExecutor;
            _serviceProvider = serviceProvider;
        }

        public async Task<CommandResult> Execute<TModule, TCommand>(TCommand command, CancellationToken cancellationToken)
            where TModule : EasyWayModule
            where TCommand : Command
        {
            var logger = _serviceProvider.GetRequiredService<EasyWayLogger<TModule>>();

            //TODO begin scope (correlation Id, userId)

            logger.Executing(command);

            try
            {
                var result = await _decoratedCommandExecutor.Execute<TModule, TCommand>(command, cancellationToken);

                switch (result.Error)
                {
                    case CommandErrorEnum.None: logger.Successed(); break;
                    case CommandErrorEnum.Validation: logger.Validation(result.ValidationErrors); break;
                    case CommandErrorEnum.BrokenBusinessRule: logger.BrokenBusinessRule(result.BrokenBusinessRuleException.BrokenBusinessRule); break;
                    case CommandErrorEnum.ConcurrencyConflict: logger.ConcurrencyConflict(result.Exception); break;
                    case CommandErrorEnum.OperationCanceled: logger.OperationCanceled(); break;
                    case CommandErrorEnum.NotFound: logger.NotFound(); break;
                    case CommandErrorEnum.Forbidden: logger.Forbidden(); break;
                    default: logger.UnexpectedException(result.Exception); break;
                }

                return result;
            }
            catch (Exception ex)
            {
                logger.UnexpectedException(ex);

                return CommandResult.UnknownException(ex);
            }
        }
    }
}
