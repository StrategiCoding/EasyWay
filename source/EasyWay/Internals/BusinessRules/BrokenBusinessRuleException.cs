namespace EasyWay.Internals.BusinessRules
{
    internal sealed class BrokenBusinessRuleException : EasyWayException
    {
        internal BusinessRule BrokenBusinessRule { get; }

        internal BrokenBusinessRuleException(BusinessRule brokenBusinessRule)
            : base(brokenBusinessRule.Message)
        {
            BrokenBusinessRule = brokenBusinessRule;
        }

        public sealed override string ToString() => BrokenBusinessRule.ToString();
    }
}
