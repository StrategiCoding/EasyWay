using Microsoft.AspNetCore.Http;
using System.Net;
using EasyWay.Internals.Cookies;
using Microsoft.Extensions.Logging;

namespace EasyWay.Internals.Middlewares
{
    internal sealed class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IRefreshTokenCookie _refreshTokenCookie;

        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(
            RequestDelegate next,
            IRefreshTokenCookie refreshTokenCookie,
            ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _refreshTokenCookie = refreshTokenCookie;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                _logger.LogError("Exception {@exception}", exception);

                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}
