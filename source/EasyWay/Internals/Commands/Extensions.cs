using System.Reflection;
using EasyWay.Internals.Commands.Commands;
using EasyWay.Internals.Commands.CommandsWithResult;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Commands
{
    internal static class Extensions
    {
        internal static IServiceCollection AddCommands(
            this IServiceCollection services,
            Type moduleType,
            IEnumerable<Assembly> assemblies)
        {
            services
                .AddCommandExecutor()
                .AddCommandExecutorWithResult();

            services.AddAsBasedType(typeof(CommandHandler<>), ServiceLifetime.Scoped, assemblies);

            services.AddAsBasedType(typeof(CommandHandler<,>), ServiceLifetime.Scoped, assemblies);

            services.AddScoped<UnitOfWork>();

            services.AddSingleton(new ConcurrencyConflictValidator());

            return services;
        }
    }
}
