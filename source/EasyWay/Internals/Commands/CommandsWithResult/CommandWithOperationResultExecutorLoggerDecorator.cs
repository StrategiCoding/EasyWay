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

        public Task<CommandResult<TOperationResult>> Command<TModule, TCommand, TOperationResult>(TCommand command, CancellationToken cancellationToken = default)
            where TModule : EasyWayModule
            where TCommand : Command<TOperationResult>
            where TOperationResult : OperationResult
        {
            return _decoratedCommandExecutor.Command<TModule, TCommand, TOperationResult>(command, cancellationToken);
        }
    }
}
