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

        public void AddModule<TModule>()
            where TModule : EasyWayModule, new()
        {
            var services = AppBuilder.Services;
            var configuration = AppBuilder.Configuration.GetSection(typeof(TModule).Name);

            var module = new TModule();

            module.Initialize<TModule>(services, configuration);
        }

        public WebKernel Build()
        {
            return new WebKernel(AppBuilder.Build());
        }
    }
}
