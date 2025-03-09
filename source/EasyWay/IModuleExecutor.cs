namespace EasyWay
{
    public interface IModuleExecutor<TModule>
        where TModule : EasyWayModule
    {
        Task<CommandResult> Command<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : Command;

        Task<CommandResult<TOperationResult>> Command<TCommand, TOperationResult>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : Command<TOperationResult>
            where TOperationResult : OperationResult;

        Task<QueryResult<TReadModel>> Query<TQuery, TReadModel>(TQuery query, CancellationToken cancellationToken = default)
            where TQuery : Query<TReadModel>
            where TReadModel : ReadModel;
    }
}
