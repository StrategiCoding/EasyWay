using EasyWay.Internals.Domain.SeedWorks.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EasyWay.Internals.Contracts
{
    internal sealed class SecurityActionExecutor : ISecurityActionExecutor
    {
        private readonly IServiceProvider _serviceProvider;

        public SecurityActionExecutor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<SecurityResult> Execute<TAction>(TAction action)
            where TAction : class, ISecurityAction
        {
            var result = await _serviceProvider.GetRequiredService<ISecurityActionHandler<TAction>>().Handle(action);

            if (result.IsFailure)
            {
                var logger = _serviceProvider.GetRequiredService<ILogger<TAction>>();

                logger.LogWarning(result.Error.Code);
            }

            return result;
        }

        public async Task<SecurityResult<TResult>> Execute<TAction, TResult>(TAction action)
            where TAction : class, ISecurityAction<TResult>
        {
            var result = await _serviceProvider.GetRequiredService<ISecurityActionHandler<TAction,TResult>>().Handle(action);

            if (result.IsFailure)
            {
                var logger = _serviceProvider.GetRequiredService<ILogger<TAction>>();

                logger.LogWarning(result.Error.Code);
            }

            return result;
        }
    }
}
