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

        Task<QueryResult<TReadModel>> ExecuteQuery<TQuery, TReadModel>(TQuery query, CancellationToken cancellationToken = default)
            where TQuery : Query<TModule, TReadModel>
            where TReadModel : ReadModel;
    }
}
