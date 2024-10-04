using EasyWay.Internals.Commands;
using EasyWay.Internals.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Modules
{
    internal sealed class ModuleExecutor<TModule> : IModuleExecutor<TModule>
        where TModule : Module
    {
        private readonly IServiceProvider _serviceProvider;

        public ModuleExecutor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Task Execute<TCommand>(TCommand command, CancellationToken cancellationToken = default) 
            where TCommand : Command
        {
            return _serviceProvider.GetRequiredService<ICommandExecutor>().Execute(command, cancellationToken);
        }

        public Task<TResult> Execute<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default)
            where TQuery : Query<TResult>
            where TResult : ReadModel
        {
            return _serviceProvider.GetRequiredService<IQueryExecutor>().Execute<TQuery, TResult>(query, cancellationToken);
        }
    }
}
