using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.BusinessRules
{
    internal sealed class BrokenBusinessRuleDispacher : IBrokenBusinessRuleDispacher
    {
        private readonly IServiceProvider _serviceProvider;

        public BrokenBusinessRuleDispacher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Dispach<TBrokenBusinessRule>(TBrokenBusinessRule brokenBusinessRule) 
            where TBrokenBusinessRule : BusinessRule
        {
            var handlerType = typeof(IBrokenBusinessRuleHandler<>).MakeGenericType(brokenBusinessRule.GetType());

            var handlers = _serviceProvider.GetServices(handlerType);

            foreach (var handler in handlers)
            {
                await(Task)handlerType
                    .GetMethod(nameof(IBrokenBusinessRuleHandler<TBrokenBusinessRule>.Handle))?
                    .Invoke(handler, new object[] { brokenBusinessRule });
            }
        }
    }
}
