namespace EasyWay.Internals.Commands.CommandsWithResult
{
    internal interface ICommandWithOperationResultExecutor
    {
        Task<CommandResult<TOperationResult>> Command<TModule, TCommand, TOperationResult>(TCommand command, CancellationToken cancellationToken = default)
            where TModule : EasyWayModule
            where TCommand : Command<TOperationResult>
            where TOperationResult : OperationResult;
    }
}
