using Microsoft.Extensions.DependencyInjection;

namespace EasyWay
{
    public static class Kernel
    {
        private static readonly IServiceCollection services = new ServiceCollection();

        private static IEnumerable<IModuleExecutor<Module>> _modules = new List<IModuleExecutor<Module>>();

        public static void Add<TModule>()
            where TModule : Module, new()
        {
            var x = new TModule();

            x.Initialize<TModule>(services);
        }
    }
}
