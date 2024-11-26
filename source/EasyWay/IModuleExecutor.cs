namespace EasyWay
{
    public interface IModuleExecutor<TModule>
        where TModule : EasyWayModule
    {
        Task<CommandResult> Execute<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : Command<TModule>;

        Task<CommandResult<TOperationResult>> Execute<TOperationResult>(Command<TModule, TOperationResult> command, CancellationToken cancellationToken = default)
            where TOperationResult : OperationResult;

        Task<QueryResult<TReadModel>> Execute<TReadModel>(Query<TModule, TReadModel> query, CancellationToken cancellationToken = default)
            where TReadModel : ReadModel;
    }
}
