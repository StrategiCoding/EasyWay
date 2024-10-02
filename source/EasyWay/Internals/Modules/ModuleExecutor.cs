using EasyWay.Internals.Commands;
using EasyWay.Internals.Queries;

namespace EasyWay.Internals.Modules
{
    internal sealed class ModuleExecutor<TModule> : IModuleExecutor<TModule>
        where TModule : Module
    {
        private readonly ICommandExecutor _commandExecutor;

        private readonly IQueryExecutor _queryExecutor;

        public ModuleExecutor(
            ICommandExecutor commandExecutor,
            IQueryExecutor queryExecutor)
        {
            _commandExecutor = commandExecutor;
            _queryExecutor = queryExecutor;
        }

        public Task Execute<TCommand>(TCommand command, CancellationToken cancellationToken = default) 
            where TCommand : Command
        {
            return _commandExecutor.Execute(command, cancellationToken);
        }

        public Task<TResult> Execute<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default)
            where TQuery : Query<TResult>
            where TResult : ReadModel
        {
            return _queryExecutor.Execute<TQuery, TResult>(query, cancellationToken);
        }
    }
}
