using EasyWay.Internals;
using EasyWay.Internals.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyWay
{
    public abstract class EasyWayModule
    {
        internal void Initialize<TModule>(IServiceCollection services, IConfiguration configuration)
            where TModule : EasyWayModule
        {
            services.AddEasyWay<TModule>(Assemblies);

            ConfigureDependencies(services, configuration);

            services.AddSingleton<IModuleExecutor<TModule>, ModuleExecutor<TModule>>();
        }

        protected abstract IEnumerable<Assembly> Assemblies { get; }

        protected abstract void ConfigureDependencies(IServiceCollection services, IConfiguration configuration);
    }
}
