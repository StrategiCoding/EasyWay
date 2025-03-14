﻿using EasyWay.Internals.BusinessRules;
using Microsoft.AspNetCore.Http;

namespace EasyWay.Internals.Exceptions
{
    internal sealed class BrokenBusinessRuleExceptionResponse : ExceptionResponse
    {
        internal BrokenBusinessRuleExceptionResponse(Exception ex)
        {
            Type = "BrokenBusinessRule";
            Detail = ex.Message;
            Status = StatusCodes.Status409Conflict;
        }
    }
}
