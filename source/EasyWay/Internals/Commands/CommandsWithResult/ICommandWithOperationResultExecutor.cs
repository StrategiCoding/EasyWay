namespace EasyWay.Internals.Commands.CommandsWithResult
{
    internal interface ICommandWithOperationResultExecutor<TModule>
        where TModule : EasyWayModule
    {
        Task<CommandResult<TOperationResult>> Command<TCommand, TOperationResult>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : Command<TOperationResult>
            where TOperationResult : OperationResult;
    }
}
