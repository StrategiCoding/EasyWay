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

        public void AddModue<TModule>()
            where TModule : Module, new()
        {

        }

        public WebKernel Build()
        {

            return new WebKernel(AppBuilder.Build());
        }
    }
}
