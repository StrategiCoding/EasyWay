 using EasyWay.Internals.Commands;
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

        public async Task<CommandResult> Execute<TCommand>(TCommand command, CancellationToken cancellationToken = default) 
            where TCommand : Command
        {
            CommandResult commandResult;

            using (var scope = _serviceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;

                commandResult = await sp
                    .GetRequiredService<ICommandExecutor<TModule>>()
                    .Execute(command, cancellationToken);
            }

            return commandResult;
        }

        public async Task<CommandResult<TOperationResult>> Execute<TOperationResult>(Command<TOperationResult> command, CancellationToken cancellationToken = default)
            where TOperationResult : OperationResult
        {
            CommandResult<TOperationResult> commandResult;

            using (var scope = _serviceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;

                commandResult = await sp
                    .GetRequiredService<ICommandWithOperationResultExecutor<TModule>>()
                    .Execute(command, cancellationToken);
            }

            return commandResult;
        }

        public async Task<QueryResult<TReadModel>> Execute<TReadModel>(Query<TReadModel> query, CancellationToken cancellationToken = default)
            where TReadModel : ReadModel
        {
            QueryResult<TReadModel> result;

            using (var scope = _serviceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;

                result = await sp
                    .GetRequiredService<IQueryExecutor<TModule>>()
                    .Execute(query, cancellationToken);
            }

            return result;
        }
    }
}
