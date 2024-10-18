using Microsoft.AspNetCore.Http;

namespace EasyWay.Internals.Exceptions
{
    internal sealed class ConcurrencyExceptionResponse : ExceptionResponse
    {
        internal ConcurrencyExceptionResponse(ConcurrencyException ex)
        {
            Type = "ConcurrencyConflict";
            Detail = "The resource has been modified by another process (try again)";
            Status = StatusCodes.Status409Conflict;
        }
    }
}
