using Microsoft.Extensions.DependencyInjection;

namespace EasyWay
{
    public static class ModuleFactory
    {
        public static IModuleExecutor<TModule> Create<TModule>()
            where TModule : Module, new()
        {
            var x = new TModule();

            var container = x.Initialize<TModule>();

            return container.GetRequiredService<IModuleExecutor<TModule>>();
        }
    }
}
