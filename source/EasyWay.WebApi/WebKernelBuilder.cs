using EasyWay.Internals;
using Microsoft.AspNetCore.Builder;

namespace EasyWay
{
    public class WebKernelBuilder
    {
        public readonly WebApplicationBuilder AppBuilder;

        internal WebKernelBuilder(WebApplicationBuilder webApplicationBuilder) 
        {
            AppBuilder = webApplicationBuilder;
        }

        public void AddModule<TModule, TModuleConfigurator>()
            where TModule : EasyWayModule
            where TModuleConfigurator : ModuleConfigurator<TModule>, new()
        {
            var services = AppBuilder.Services;
            var configuration = AppBuilder.Configuration.GetSection(typeof(TModule).Name);

            var moduleConfigurator = new TModuleConfigurator();

            moduleConfigurator.Initialize(services, configuration);
        }

        public WebKernel Build()
        {
            AppBuilder.Services.AddEasyWayWebApi();

            return new WebKernel(AppBuilder.Build());
        }
    }
}
