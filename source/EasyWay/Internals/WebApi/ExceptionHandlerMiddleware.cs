using EasyWay.Internals.UnitOfWorks;
using EasyWay.Internals.WebApi.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EasyWay.Internals.WebApi
{
    internal sealed class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(httpContext, exception);
            }
        }
        private static Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
        {
            var exceptionType = exception.GetType();

            ExceptionResponse exceptionResponse = new InternalServerExceptionResponse();

            switch (exception)
            {
                case BusinessRuleException e when exceptionType == typeof(BusinessRuleException):
                    exceptionResponse = new BusinessRuleExceptionResponse(e);
                    break;
                case ConcurrencyException e when exceptionType == typeof(ConcurrencyException):
                    exceptionResponse = new ConcurrencyExceptionResponse(e);
                    break;
            }

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = exceptionResponse.Status;

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            var response = JsonSerializer.Serialize(exceptionResponse, options);

            return httpContext.Response.WriteAsync(response);
        }
    }
}
