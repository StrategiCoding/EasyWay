using EasyWay.Internals.Commands.Commands;
using EasyWay.Internals.Commands.CommandsWithResult;
using EasyWay.Internals.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Modules
{
    internal sealed class ModuleExecutor<TModule> : IModuleExecutor<TModule>
        where TModule : EasyWayModule
    {
        private readonly IServiceProvider _serviceProvider;

        public ModuleExecutor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<CommandResult> Command<TCommand>(TCommand command, CancellationToken cancellationToken = default) 
            where TCommand : Command
        {
            CommandResult commandResult;

            await using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var sp = scope.ServiceProvider;

                commandResult = await sp
                    .GetRequiredService<ICommandExecutor<TModule>>()
                    .Execute(command, cancellationToken);
            }

            return commandResult;
        }

        public async Task<CommandResult<TOperationResult>> Command<TCommand, TOperationResult>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : Command<TOperationResult>
            where TOperationResult : OperationResult
        {
            CommandResult<TOperationResult> commandResult;

            await using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var sp = scope.ServiceProvider;

                commandResult = await sp
                    .GetRequiredService<ICommandWithOperationResultExecutor<TModule>>()
                    .Command<TCommand, TOperationResult>(command, cancellationToken);
            }

            return commandResult;
        }

        public async Task<QueryResult<TReadModel>> Query<TQuery, TReadModel>(TQuery query, CancellationToken cancellationToken = default)
            where TQuery : Query<TReadModel>
            where TReadModel : ReadModel
        {
            QueryResult<TReadModel> result;

            await using (var scope = _serviceProvider.CreateAsyncScope())
            {
                var sp = scope.ServiceProvider;

                result = await sp
                    .GetRequiredService<IQueryExecutor>()
                    .Execute<TModule, TQuery, TReadModel>(query, cancellationToken);
            }

            return result;
        }
    }
}
