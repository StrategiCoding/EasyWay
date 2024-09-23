namespace EasyWay.Internals.Commands
{
    internal interface ICommandExecutor
    {
        Task Execute<TCommand>(TCommand command, CancellationToken cancellationToken)
            where TCommand : Command;
    }
}
