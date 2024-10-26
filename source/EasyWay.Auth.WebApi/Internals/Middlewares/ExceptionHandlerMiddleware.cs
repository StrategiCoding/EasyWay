using Microsoft.AspNetCore.Http;
using EasyWay.Internals.Domain.Exceptions;
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
            catch (AuthException authException)
            {
                //TODO log WARNING SECURITY
                //TODO remove refresh token from storage
                _logger.LogWarning("SECURITY ALERT {@name}", authException.GetType().Name);

                _refreshTokenCookie.Remove(httpContext);
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            catch (Exception exception)
            {
                //TODO log ERROR
                _logger.LogError("Exception {@exception}", exception);
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}
