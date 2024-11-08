namespace EasyWay.Internals.Commands
{
    internal interface ICommandExecutor<TModule>
        where TModule : EasyWayModule
    {
        Task<CommandResult> Execute<TCommand>(TCommand command, CancellationToken cancellationToken)
            where TCommand : Command<TModule>;

        Task<CommandResult<TOperationResult>> Execute<TCommand, TOperationResult>(TCommand command, CancellationToken cancellationToken)
            where TCommand : Command<TModule, TOperationResult>
            where TOperationResult : OperationResult;
    }
}
