namespace EasyWay.Internals.Commands.Commands
{
    internal interface ICommandExecutor<TModule>
        where TModule : EasyWayModule
    {
        Task<CommandResult> Execute<TCommand>(TCommand command, CancellationToken cancellationToken)
            where TCommand : Command;
    }
}
