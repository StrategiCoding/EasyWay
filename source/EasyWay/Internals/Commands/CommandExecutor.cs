using EasyWay.Internals.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Commands
{
    internal sealed class CommandExecutor : ICommandExecutor
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandExecutor(IServiceProvider serviceProvider) 
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Execute<TCommand>(TCommand command, CancellationToken cancellationToken)
            where TCommand : Command
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var sp = scope.ServiceProvider;

                sp
                .GetRequiredService<CancellationContext>()
                .Set(cancellationToken);

                await sp
                .GetRequiredService<ICommandHandler<TCommand>>()
                .Handle(command)
                .ConfigureAwait(false);
            }
        }
    }
}
