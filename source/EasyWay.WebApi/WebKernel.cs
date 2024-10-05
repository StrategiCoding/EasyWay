using EasyWay.Internals;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace EasyWay
{
    public class WebKernel
    {
        public static WebKernelBuilder CreateBuilder(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.UseKestrel(option => option.AddServerHeader = false);

            return new WebKernelBuilder(builder);
        }

        public readonly WebApplication App;

        internal WebKernel(WebApplication webApplication) 
        {
            App = webApplication;

            App.UseEasyWay();
        }

        public async Task RunAsync()
        {
            await App.RunAsync();
        }
    }
}
