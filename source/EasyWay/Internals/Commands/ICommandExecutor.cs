namespace EasyWay.Internals.Commands
{
    internal interface ICommandExecutor<TModule>
        where TModule : EasyWayModule
    {
        Task<CommandResult> Execute<TCommand>(TCommand command, CancellationToken cancellationToken)
            where TCommand : Command<TModule>;

        Task<TCommandResult> Execute<TCommand, TCommandResult>(TCommand command, CancellationToken cancellationToken)
            where TCommand : Command<TModule, TCommandResult>
            where TCommandResult : OperationResult;
    }
}
