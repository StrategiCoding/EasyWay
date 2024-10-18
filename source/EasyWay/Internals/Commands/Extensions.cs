using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Commands
{
    internal static class Extensions
    {
        internal static IServiceCollection AddCommands<TModule>(
            this IServiceCollection services,
            IEnumerable<Assembly> assemblies)
            where TModule : EasyWayModule
        {
            services.AddScoped<ICommandExecutor<TModule>, CommandExecutor<TModule>>();

            services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            services.AddScoped<IUnitOfWorkCommandHandler, UnitOfWorkCommandHandler>();

            services.AddSingleton<IConcurrencyConflictValidator, ConcurrencyConflictValidator>();

            return services;
        }
    }
}
