using Microsoft.AspNetCore.Http;

namespace EasyWay.Internals.Exceptions
{
    internal sealed class OperationCanceledExceptionResponse : ExceptionResponse
    {
        internal OperationCanceledExceptionResponse(OperationCanceledException ex)
        {
            Type = "OperationCanceled";
            Detail = "Operation has been cancelled";
            Status = StatusCodes.Status499ClientClosedRequest;
        }
    }
}
