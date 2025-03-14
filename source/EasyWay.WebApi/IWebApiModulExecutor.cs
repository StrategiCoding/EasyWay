using Microsoft.AspNetCore.Http;

namespace EasyWay
{
    public interface IWebApiModulExecutor<TModule>
        where TModule : EasyWayModule
    {
        Task<IResult> Command<TCommand>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : Command;

        Task<IResult> Command<TCommand, TOperationResult>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : Command<TOperationResult>
            where TOperationResult : OperationResult;

        Task<IResult> Query<TQuery, TReadModel>(TQuery query, CancellationToken cancellationToken = default)
            where TQuery : Query<TReadModel>
            where TReadModel : ReadModel;
    }
}
