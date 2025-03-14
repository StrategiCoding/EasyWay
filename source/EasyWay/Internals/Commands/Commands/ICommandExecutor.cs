namespace EasyWay.Internals.Commands.Commands
{
    internal interface ICommandExecutor
    {
        Task<CommandResult> Execute<TModule, TCommand>(TCommand command, CancellationToken cancellationToken)
            where TModule : EasyWayModule
            where TCommand : Command;
    }
}
