using Microsoft.AspNetCore.Http;

namespace EasyWay.Internals.Exceptions
{
    internal sealed class InternalServerExceptionResponse : ExceptionResponse
    {
        internal InternalServerExceptionResponse()
        {
            Type = "InternalServerError";
            Detail = "Something went wrong";
            Status = StatusCodes.Status500InternalServerError;
        }
    }
}
