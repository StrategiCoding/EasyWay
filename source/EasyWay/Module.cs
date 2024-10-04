using EasyWay.Internals.Modules;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyWay
{
    public abstract class Module
    {
        internal void Initialize<TModule>(IServiceCollection services)
            where TModule : Module, new()
        {

            services.AddEasyWay(Assemblies);

            ConfigureDependencies(services);

            services.AddSingleton<IModuleExecutor<TModule>, ModuleExecutor<TModule>>();
        }

        protected abstract IEnumerable<Assembly> Assemblies { get; }

        protected abstract void ConfigureDependencies(IServiceCollection services);
    }
}
