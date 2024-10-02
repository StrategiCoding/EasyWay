using EasyWay.Internals.Modules;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyWay
{
    public abstract class Module
    {
        internal IServiceProvider Initialize<TModule>()
            where TModule : Module, new()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddEasyWay(Assemblies);

            ConfigureDependencies(serviceCollection);

            serviceCollection.AddSingleton<IModuleExecutor<TModule>, ModuleExecutor<TModule>>();

            return serviceCollection.BuildServiceProvider();
        }

        protected abstract IEnumerable<Assembly> Assemblies { get; }

        protected abstract void ConfigureDependencies(IServiceCollection services);
    }
}
