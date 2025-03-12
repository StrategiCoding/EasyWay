using Microsoft.Extensions.Logging;

namespace EasyWay.Internals.Queries.Loggers
{
    internal sealed partial class EasyWayLogger<TModule>
        where TModule : EasyWayModule
    {
        private readonly ILogger _logger;

        public EasyWayLogger(ILogger<TModule> logger) => _logger = logger;

        //TODO scope logger CorrelationId (ScopeId)

        [LoggerMessage(0, LogLevel.Information, "Executing {@component}", SkipEnabledCheck = true)]
        public partial void Executing(object component);

        [LoggerMessage(1, LogLevel.Information, "Executed", SkipEnabledCheck = true)]
        public partial void Executed();

        [LoggerMessage(2, LogLevel.Information, "Failed", SkipEnabledCheck = true)]
        public partial void Failed();

        [LoggerMessage(3, LogLevel.Error, "Unexpected exception", SkipEnabledCheck = true)]
        public partial void UnexpectedException(Exception exception);
    }
}
