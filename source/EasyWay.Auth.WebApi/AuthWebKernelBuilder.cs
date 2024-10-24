using Microsoft.AspNetCore.Builder;
using EasyWay.Internals;

namespace EasyWay
{
    public class AuthWebKernelBuilder
    {
        internal readonly WebApplicationBuilder AppBuilder;

        internal AuthWebKernelBuilder(WebApplicationBuilder webApplicationBuilder)
        {
            AppBuilder = webApplicationBuilder;
            AppBuilder.Services.AddAuth();
        }

        public AuthWebKernel Build()
        {
            return new AuthWebKernel(AppBuilder.Build());
        }
    }
}
