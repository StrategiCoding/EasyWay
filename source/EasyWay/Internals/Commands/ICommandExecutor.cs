namespace EasyWay.Internals.Commands
{
    internal interface ICommandExecutor<TModule>
        where TModule : EasyWayModule
    {
        Task Execute<TCommand>(TCommand command, CancellationToken cancellationToken)
            where TCommand : Command<TModule>;
    }
}
