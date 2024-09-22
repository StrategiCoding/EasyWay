using EasyWay.Samples.Domain;

namespace EasyWay.Samples.BrokenBusinessRules
{
    internal sealed class BrokenSampleBusinessRuleHandler2 : IBrokenBusinessRuleHandler<SampleBusinessRule>
    {
        public Task Handle(SampleBusinessRule brokenBusinessRule)
        {
            return Task.CompletedTask;
        }
    }
}
