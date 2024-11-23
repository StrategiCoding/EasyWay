using EasyWay.Internals;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace EasyWay
{
    public sealed class WebKernelBuilder
    {
        public readonly WebApplicationBuilder AppBuilder;

        private readonly Kernel _kernel;

        private WebKernelBuilder(WebApplicationBuilder webApplicationBuilder) 
        {
            AppBuilder = webApplicationBuilder;
            _kernel = Kernel.Create();
        }

        public static WebKernelBuilder Create(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.UseKestrel(option => option.AddServerHeader = false);

            return new WebKernelBuilder(builder);
        }

        public void AddModule<TModule>()
            where TModule : EasyWayModule, new()
        {
            _kernel.AddModule<TModule>();
        }

        public async Task<WebKernel> BuildAsync()
        {
            await _kernel.BuildAsync(AppBuilder.Services);
            AppBuilder.Services.AddEasyWayWebApi();


            return new WebKernel(AppBuilder.Build());
        }
    }
}
