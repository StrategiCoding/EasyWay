using System.Collections.Generic;

namespace EasyWay
{
    public interface IModuleExecutor<TModule>
        where TModule : EasyWayModule
    {
        Task Execute<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : Command<TModule>;

        Task<TResult> Execute<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default)
            where TQuery : Query<TModule, TResult>
            where TResult : ReadModel;
    }
}
