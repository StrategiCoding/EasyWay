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

        public async Task ExecuteCommand<TCommand>(TCommand command, CancellationToken cancellationToken = default) 
            where TCommand : Command<TModule>
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;

                await sp
                    .GetRequiredService<ICommandExecutor<TModule>>()
                    .Execute(command, cancellationToken);
            }
        }

        public async Task<TCommandResult> ExecuteCommand<TCommand, TCommandResult>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : Command<TModule, TCommandResult>
            where TCommandResult : CommandResult
        {
            TCommandResult commandResult;

            using (var scope = _serviceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;

                commandResult = await sp
                    .GetRequiredService<ICommandExecutor<TModule>>()
                    .Execute<TCommand, TCommandResult>(command, cancellationToken);
            }

            return commandResult;
        }

        public async Task<QueryResult<TReadModel>> ExecuteQuery<TQuery, TReadModel>(TQuery query, CancellationToken cancellationToken = default)
            where TQuery : Query<TModule, TReadModel>
            where TReadModel : ReadModel
        {
            QueryResult<TReadModel> result;

            using (var scope = _serviceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;

                result = await sp
                    .GetRequiredService<IQueryExecutor<TModule>>()
                    .Execute<TQuery, TReadModel>(query, cancellationToken);
            }

            return result;
        }
    }
}
