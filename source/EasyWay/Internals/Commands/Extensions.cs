using System.Reflection;
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
            services.AddScoped(typeof(ICommandExecutor<>).MakeGenericType(moduleType), typeof(CommandExecutor<>).MakeGenericType(moduleType));
            services.AddScoped(typeof(ICommandWithOperationResultExecutor<>).MakeGenericType(moduleType), typeof(CommandWithOperationResultExecutor<>).MakeGenericType(moduleType));

            services.AddAsBasedType(typeof(CommandHandler<>), ServiceLifetime.Scoped, assemblies);

            services.AddAsBasedType(typeof(CommandHandler<,>), ServiceLifetime.Scoped, assemblies);

            services.AddScoped<IUnitOfWorkCommandHandler, UnitOfWorkCommandHandler>();

            services.AddSingleton(new ConcurrencyConflictValidator());

            return services;
        }
    }
}
