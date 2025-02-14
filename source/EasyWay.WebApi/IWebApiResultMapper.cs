using Microsoft.AspNetCore.Http;

namespace EasyWay
{
    public interface IWebApiResultMapper
    {
        IResult Map(CommandResult commandResult);

        IResult Map<TOperationResult>(CommandResult<TOperationResult> commandResult)
            where TOperationResult : OperationResult;

        IResult Map<TReadModel>(QueryResult<TReadModel> queryResult)
            where TReadModel : ReadModel;
    }
}
