using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Commands.Commands
{
    internal static class Extensions
    {
        internal static IServiceCollection AddCommandExecutor(this IServiceCollection services)
        {
            services.AddScoped<CommandExecutor>();

            services.AddScoped<ICommandExecutor>(p =>
            {
                var executor = p.GetRequiredService<CommandExecutor>();

                var logger = new CommandExecutorLoggerDecorator(executor, p);

                return logger;
            });

            return services;
        }
    }
}
