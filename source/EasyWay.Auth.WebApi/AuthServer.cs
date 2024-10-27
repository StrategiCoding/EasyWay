using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using EasyWay.Internals;
using EasyWay.Internals.Middlewares;

namespace EasyWay
{
    public static class AuthServer
    {
        public static async Task RunAsync(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.WebHost.UseKestrel(option => option.AddServerHeader = false);

            builder.Services.AddAuth();
            builder.Services.AddAuthWebApi();

            var app = builder.Build();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.MapIssueEndpoint();
            app.MapRefreshEndpoint();
            app.MapCancelEndpoint();

            await app.RunAsync();
        }
    }
}
