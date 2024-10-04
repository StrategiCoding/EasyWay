using Microsoft.AspNetCore.Builder;

namespace EasyWay
{
    public class WebKernel
    {
        public static WebKernelBuilder CreateBuilder(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

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
