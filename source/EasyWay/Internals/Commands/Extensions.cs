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

            services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            services.AddScoped<IUnitOfWorkCommandHandler, UnitOfWorkCommandHandler>();

            services.AddSingleton<IConcurrencyConflictValidator, ConcurrencyConflictValidator>();

            return services;
        }
    }
}
