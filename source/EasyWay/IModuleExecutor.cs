namespace EasyWay
{
    public interface IModuleExecutor<TModule>
        where TModule : EasyWayModule
    {
        Task ExecuteCommand<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : Command<TModule>;

        Task<TCommandResult> ExecuteCommand<TCommand, TCommandResult>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : Command<TModule, TCommandResult>
            where TCommandResult : CommandResult;

        Task<TResult> ExecuteQuery<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default)
            where TQuery : Query<TModule, TResult>
            where TResult : ReadModel;
    }
}
