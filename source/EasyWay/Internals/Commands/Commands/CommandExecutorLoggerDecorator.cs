using EasyWay.Internals.Queries.Loggers;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Commands.Commands
{
    internal sealed class CommandExecutorLoggerDecorator : ICommandExecutor
    {
        private readonly ICommandExecutor _decoratedCommandExecutor;

        private readonly IServiceProvider _serviceProvider;

        public CommandExecutorLoggerDecorator(
            ICommandExecutor decoratedCommandExecutor,
            IServiceProvider serviceProvider)
        {
            _decoratedCommandExecutor = decoratedCommandExecutor;
            _serviceProvider = serviceProvider;
        }

        public async Task<CommandResult> Execute<TModule, TCommand>(TCommand command, CancellationToken cancellationToken)
            where TModule : EasyWayModule
            where TCommand : Command
        {
            var logger = _serviceProvider.GetRequiredService<EasyWayLogger<TModule>>();

            //TODO begin scope (correlation Id)

            logger.Executing(command);

            try
            {
                var result = await _decoratedCommandExecutor.Execute<TModule, TCommand>(command, cancellationToken);
                // TODO warning when forbidden
                logger.Executed();

                return result;
            }
            catch (Exception ex)
            {
                logger.UnexpectedException(ex);
                throw;
            }
        }
    }
}
