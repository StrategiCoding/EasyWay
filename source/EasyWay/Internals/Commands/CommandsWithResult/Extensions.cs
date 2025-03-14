using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Commands.CommandsWithResult
{
    internal static class Extensions
    {
        internal static IServiceCollection AddCommandExecutorWithResult(this IServiceCollection services)
        {
            services.AddScoped<CommandWithOperationResultExecutor>();

            services.AddScoped<ICommandWithOperationResultExecutor>(p =>
            {
                var executor = p.GetRequiredService<CommandWithOperationResultExecutor>();

                return new CommandWithOperationResultExecutorLoggerDecorator(executor, p);
            });
            return services;
        }
    }
}
