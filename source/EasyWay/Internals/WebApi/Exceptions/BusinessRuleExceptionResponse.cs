using EasyWay.Internals.BusinessRules;
using Microsoft.AspNetCore.Http;

namespace EasyWay.Internals.WebApi.Exceptions
{
    internal sealed class BusinessRuleExceptionResponse : ExceptionResponse
    {
        internal BusinessRuleExceptionResponse(BusinessRuleException ex)
        {
            Type = "BrokenBusinessRule";
            Detail = ex.Message;
            Status = StatusCodes.Status409Conflict;
        }
    }
}
