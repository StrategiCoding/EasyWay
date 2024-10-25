using Microsoft.AspNetCore.Http;
using EasyWay.Internals.Domain.Exceptions;
using System.Net;
using EasyWay.Internals.Cookies;

namespace EasyWay.Internals.Middlewares
{
    internal sealed class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly IRefreshTokenCookie _refreshTokenCookie;

        public ExceptionHandlerMiddleware(
            RequestDelegate next,
            IRefreshTokenCookie refreshTokenCookie)
        {
            _next = next;
            _refreshTokenCookie = refreshTokenCookie;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (ForbiddenException forbiddenException)
            {
                //TODO log WARNING SECURITY
                //TODO remove refresh token from storage
                _refreshTokenCookie.Remove(httpContext);
                httpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            catch (Exception exception)
            {
                //TODO log ERROR
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }
    }
}
