using EasyWay.Internals;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay
{
    public abstract class TestBase<TModule, TModuleConfigurator>
        where TModule : EasyWayModule
        where TModuleConfigurator : ModuleConfigurator<TModule>, new()
    {
        private readonly IServiceCollection _services = new ServiceCollection();

        protected TestBase() 
        {
            var kernel = Kernel.Create();

            kernel.AddModule<TModule, TModuleConfigurator>();

            var conteinerForTest = new ServiceCollection();

            kernel.BuildAsync(_services).Wait();
        }

        public IGiven Given(Action<IGivenBuilder> action)
        {
            return new Given();
        }
    }
}
