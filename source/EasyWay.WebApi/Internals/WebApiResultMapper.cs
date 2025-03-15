using EasyWay.Internals.Commands;
using EasyWay.Internals.Exceptions;
using EasyWay.Internals.Queries.Results;
using Microsoft.AspNetCore.Http;

namespace EasyWay.Internals
{
    internal sealed class WebApiResultMapper
    {
        public IResult Map(CommandResult commandResult)
        {
            return commandResult.Error switch
            {
                CommandErrorEnum.None => Results.Ok(),
                CommandErrorEnum.Validation => Results.BadRequest(commandResult.ValidationErrors),
                CommandErrorEnum.BrokenBusinessRule => Results.Conflict(new BrokenBusinessRuleExceptionResponse(commandResult.Exception)),
                CommandErrorEnum.ConcurrencyConflict => Results.StatusCode(409),
                CommandErrorEnum.OperationCanceled => Results.StatusCode(499),
                CommandErrorEnum.NotFound => Results.StatusCode(404),
                CommandErrorEnum.Forbidden => Results.StatusCode(404),
                CommandErrorEnum.UnknownException or _ => Results.StatusCode(500),
            };
        }

        public IResult Map<TOperationResult>(CommandResult<TOperationResult> commandResult) where TOperationResult : OperationResult
        {
            return commandResult.Error switch
            {
                CommandErrorEnum.None => Results.Ok(commandResult.OperationResult),
                CommandErrorEnum.Validation => Results.BadRequest(commandResult.ValidationErrors),
                CommandErrorEnum.BrokenBusinessRule => Results.Conflict(new BrokenBusinessRuleExceptionResponse(commandResult.Exception)),
                CommandErrorEnum.ConcurrencyConflict => Results.StatusCode(409),
                CommandErrorEnum.OperationCanceled => Results.StatusCode(499),
                CommandErrorEnum.NotFound => Results.StatusCode(404),
                CommandErrorEnum.Forbidden => Results.StatusCode(404),
                CommandErrorEnum.UnknownException or _ => Results.StatusCode(500),
            };
        }

        public IResult Map<TReadModel>(QueryResult<TReadModel> queryResult) where TReadModel : ReadModel
        {
            return queryResult.Error switch
            {
                QueryErrorEnum.None => Results.Ok(queryResult.ReadModel),
                QueryErrorEnum.Validation => Results.BadRequest(queryResult.ValidationErrors),
                QueryErrorEnum.OperationCanceled => Results.StatusCode(499),
                QueryErrorEnum.NotFound => Results.StatusCode(404),
                QueryErrorEnum.Forbidden => Results.StatusCode(404),
                QueryErrorEnum.UnknownException or _ => Results.StatusCode(500),
            };
        }
    }
}
