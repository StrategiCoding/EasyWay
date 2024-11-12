using EasyWay.Internals;
using Microsoft.AspNetCore.Builder;

namespace EasyWay
{
    public sealed class WebKernel
    {
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
