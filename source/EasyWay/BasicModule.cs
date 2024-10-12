using EasyWay.Internals;
using EasyWay.Internals.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyWay
{
    public abstract class BasicModule
    {
        internal void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            services.AddEasyWay(Assemblies);

            ConfigureDependencies(services, configuration);

            services.AddSingleton<IModuleExecutor, ModuleExecutor>();
        }

        protected abstract IEnumerable<Assembly> Assemblies { get; }

        protected abstract void ConfigureDependencies(IServiceCollection services, IConfiguration configuration);
    }
}
