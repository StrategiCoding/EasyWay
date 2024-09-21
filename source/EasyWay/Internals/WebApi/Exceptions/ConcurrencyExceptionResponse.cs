using EasyWay.Internals.UnitOfWorks;
using Microsoft.AspNetCore.Http;

namespace EasyWay.Internals.WebApi.Exceptions
{
    internal sealed class ConcurrencyExceptionResponse : ExceptionResponse
    {
        internal ConcurrencyExceptionResponse(ConcurrencyException ex)
        {
            Type = "ConcurrencyConflict";
            Detail = "The resource has been modified (try again)";
            Status = StatusCodes.Status409Conflict;
        }
    }
}
