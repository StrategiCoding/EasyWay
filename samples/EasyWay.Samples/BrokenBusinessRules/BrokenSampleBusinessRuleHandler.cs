using EasyWay.Samples.Domain;

namespace EasyWay.Samples.BrokenBusinessRules
{
    internal sealed class BrokenSampleBusinessRuleHandler : IBrokenBusinessRuleHandler<SampleBusinessRule>
    {
        public Task Handle(SampleBusinessRule brokenBusinessRule)
        {
            return Task.CompletedTask;
        }
    }
}
