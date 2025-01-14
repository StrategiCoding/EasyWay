﻿using EasyWay.Internals.Commands.Results;
using EasyWay.Internals.Queries.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace EasyWay
{
    public static class RcpStyleExtensions
    {
        public static WebKernel MapQuery<TModule, TQuery, TReadModel>(this WebKernel webKernel)
            where TModule : EasyWayModule
            where TQuery : Query<TModule, TReadModel>
            where TReadModel : ReadModel
        {
            webKernel.App.MapGet("api/" + typeof(TModule).Name + "/" + typeof(TQuery).Name, async ([AsParameters] TQuery query, IModuleExecutor<TModule> executor, CancellationToken cancellationToken) =>
            {
                var queryResult = await executor.Execute(query, cancellationToken);

                return queryResult.Error switch
                {
                    QueryErrorEnum.None => Results.Ok(queryResult.ReadModel),
                    QueryErrorEnum.Validation => Results.BadRequest(queryResult.ValidationErrors),
                    QueryErrorEnum.NotFound => Results.StatusCode(404),
                    QueryErrorEnum.Forbidden => Results.StatusCode(403),
                    _ => Results.StatusCode(500),
                };
            });

            return webKernel;
        }

        public static WebKernel MapCommand<TModule, TCommand>(this WebKernel webKernel)
            where TModule : EasyWayModule
            where TCommand : Command<TModule>
        {
            webKernel.App.MapPost("api/" + typeof(TModule).Name + "/" + typeof(TCommand).Name, async ([FromBody] TCommand command, IModuleExecutor<TModule> executor, CancellationToken cancellationToken) =>
            {
                var commandResult = await executor.Execute(command, cancellationToken);

                return commandResult.Error switch
                {
                    CommandErrorEnum.None => Results.Ok(),
                    CommandErrorEnum.Validation => Results.BadRequest(commandResult.ValidationErrors),
                    _ => Results.StatusCode(500),
                };
            });

            return webKernel;
        }

        public static WebKernel MapCommand<TModule, TCommand, TCommandResult>(this WebKernel webKernel)
            where TModule : EasyWayModule
            where TCommand : Command<TModule, TCommandResult>
            where TCommandResult : OperationResult
        {
            webKernel.App.MapPost("api/" + typeof(TModule).Name + "/" + typeof(TCommand).Name, async ([FromBody] TCommand command, IModuleExecutor<TModule> executor, CancellationToken cancellationToken) =>
            {
                var commandResult = await executor.Execute(command, cancellationToken);

                return commandResult.Error switch
                {
                    CommandErrorEnum.None => Results.Ok(),
                    CommandErrorEnum.Validation => Results.BadRequest(commandResult.ValidationErrors),
                    _ => Results.StatusCode(500),
                };
            });

            return webKernel;
        }
    }
}
