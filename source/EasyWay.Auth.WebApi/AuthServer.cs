using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using EasyWay.Internals;

namespace EasyWay
{
    public static class AuthServer
    {
        public static async Task RunAsync(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.UseKestrel(option => option.AddServerHeader = false);

            builder.Services.AddAuth();

            var app = builder.Build();

            app.MapIssueEndpoint();
            app.MapRefreshEndpoint();

            await app.RunAsync();
        }
    }
}
