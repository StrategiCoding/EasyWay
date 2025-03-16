using EasyWay.Internals.BusinessRules;
using Microsoft.Extensions.Logging;

namespace EasyWay.Internals.Queries.Loggers
{
    internal sealed partial class EasyWayLogger<TModule>
        where TModule : EasyWayModule
    {
        private readonly ILogger _logger;

        public EasyWayLogger(ILogger<TModule> logger) => _logger = logger;

        [LoggerMessage(0, LogLevel.Information, "Executing {@component}", SkipEnabledCheck = true)]
        public partial void Executing(object component);

        [LoggerMessage(1, LogLevel.Information, "Successed", SkipEnabledCheck = true)]
        public partial void Successed();

        [LoggerMessage(2, LogLevel.Information, "Successed: {@result}", SkipEnabledCheck = true)]
        public partial void Successed(object result);

        [LoggerMessage(3, LogLevel.Information, "Validation error: {@validatonErrors}", SkipEnabledCheck = true)]
        public partial void Validation(IDictionary<string, string[]> validatonErrors);

        [LoggerMessage(4, LogLevel.Information, "Broken business rule: {@brokenBusinessRule}", SkipEnabledCheck = true)]
        public partial void BrokenBusinessRule(BusinessRule brokenBusinessRule);

        [LoggerMessage(5, LogLevel.Information, "Not found", SkipEnabledCheck = true)]
        public partial void NotFound();

        [LoggerMessage(6, LogLevel.Information, "Operation canceled", SkipEnabledCheck = true)]
        public partial void OperationCanceled();

        [LoggerMessage(7, LogLevel.Warning, "Forbidden", SkipEnabledCheck = true)]
        public partial void Forbidden();

        [LoggerMessage(8, LogLevel.Warning, "Concurrency conflict", SkipEnabledCheck = true)]
        public partial void ConcurrencyConflict(Exception exception);

        [LoggerMessage(500, LogLevel.Error, "Unexpected exception", SkipEnabledCheck = true)]
        public partial void UnexpectedException(Exception exception);
    }
}
