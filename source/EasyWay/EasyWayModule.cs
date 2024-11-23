using EasyWay.Internals;
using EasyWay.Internals.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyWay
{
    public abstract class EasyWayModule
    {
        internal Tuple<Type, Type> Initialize(IServiceCollection services, IConfiguration configuration)
        {
            var moduleType = GetType();

            services.AddEasyWay(moduleType, Assemblies);

            ConfigureDependencies(services, configuration);

            //TODO not register in module container
            services.AddSingleton(typeof(IModuleExecutor<>).MakeGenericType(moduleType), typeof(ModuleExecutor<>).MakeGenericType(moduleType));

            return new Tuple<Type, Type>(typeof(IModuleExecutor<>).MakeGenericType(moduleType), typeof(ModuleExecutor<>).MakeGenericType(moduleType));
        }

        protected abstract IEnumerable<Assembly> Assemblies { get; }

        protected abstract void ConfigureDependencies(IServiceCollection services, IConfiguration configuration);
    }
}
