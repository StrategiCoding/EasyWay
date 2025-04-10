using EasyWay.Internals.Queries.Loggers;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Commands.CommandsWithResult
{
    internal sealed class CommandWithOperationResultExecutorLoggerDecorator : ICommandWithOperationResultExecutor
    {
        private readonly ICommandWithOperationResultExecutor _decoratedCommandExecutor;

        private readonly IServiceProvider _serviceProvider;

        public CommandWithOperationResultExecutorLoggerDecorator(
            ICommandWithOperationResultExecutor decoratedCommandExecutor,
            IServiceProvider serviceProvider)
        {
            _decoratedCommandExecutor = decoratedCommandExecutor;
            _serviceProvider = serviceProvider;
        }

        public async Task<CommandResult<TOperationResult>> Command<TModule, TCommand, TOperationResult>(TCommand command, CancellationToken cancellationToken = default)
            where TModule : EasyWayModule
            where TCommand : Command<TOperationResult>
            where TOperationResult : OperationResult
        {
            var logger = _serviceProvider.GetRequiredService<EasyWayLogger<TModule>>();

            var userContext = _serviceProvider.GetRequiredService<IUserContext>();

            //TODO begin scope (correlation Id, userId)

            if (userContext.UserId is not null)
            {
                logger.ExecutingByUser(command, userContext.UserId);
            }
            else
            {
                logger.Executing(command);
            }

            try
            {
                var result = await _decoratedCommandExecutor.Command<TModule, TCommand, TOperationResult>(command, cancellationToken);

                switch (result.Error)
                {
                    case CommandErrorEnum.None: logger.Successed(result.OperationResult); break;
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

                return CommandResult<TOperationResult>.UnknownException(ex);
            }
        }
    }
}
