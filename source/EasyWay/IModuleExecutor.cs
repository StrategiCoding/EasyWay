namespace EasyWay
{
    public interface IModuleExecutor
    {
        Task Execute<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : Command;

        Task<TResult> Execute<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default)
            where TQuery : Query<TResult>
            where TResult : ReadModel;
    }
}
