using EasyWay.Internals.Commands;
using EasyWay.Internals.Queries;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

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

        public Task Execute<TCommand>(TCommand command, CancellationToken cancellationToken = default) 
            where TCommand : Command<TModule>
        {
            return _serviceProvider.GetRequiredService<ICommandExecutor<TModule>>().Execute(command, cancellationToken);
        }

        public Task<TResult> Execute<TQuery, TResult>(TQuery query, CancellationToken cancellationToken = default)
            where TQuery : Query<TModule, TResult>
            where TResult : ReadModel
        {
            return _serviceProvider.GetRequiredService<IQueryExecutor<TModule>>().Execute<TQuery, TResult>(query, cancellationToken);
        }
    }
}
