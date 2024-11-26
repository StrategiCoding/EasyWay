namespace EasyWay.Internals.Commands
{
    internal interface ICommandWithOperationResultExecutor<TModule>
        where TModule : EasyWayModule
    {
        Task<CommandResult<TOperationResult>> Execute<TOperationResult>(Command<TModule, TOperationResult> command, CancellationToken cancellationToken)
            where TOperationResult : OperationResult;
    }
}
