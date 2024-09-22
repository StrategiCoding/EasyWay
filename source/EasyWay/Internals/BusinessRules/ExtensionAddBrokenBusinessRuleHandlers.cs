using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyWay.Internals.BusinessRules
{
    internal static class ExtensionAddBrokenBusinessRuleHandlers
    {
        internal static IServiceCollection AddBrokenBusinessRuleHandlers(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddScoped<IBrokenBusinessRuleDispacher, BrokenBusinessRuleDispacher>();

            services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IBrokenBusinessRuleHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            return services;
        }
    }
}
