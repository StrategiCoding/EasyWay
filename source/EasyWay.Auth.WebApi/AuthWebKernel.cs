using EasyWay.Internals;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace EasyWay
{
    public class AuthWebKernel
    {
        public static AuthWebKernelBuilder CreateBuilder(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.UseKestrel(option => option.AddServerHeader = false);

            return new AuthWebKernelBuilder(builder);
        }

        internal readonly WebApplication App;

        internal AuthWebKernel(WebApplication webApplication)
        {
            App = webApplication;

            App.MapIssueEndpoint();
        }

        public async Task RunAsync()
        {
            await App.RunAsync();
        }
    }
}
