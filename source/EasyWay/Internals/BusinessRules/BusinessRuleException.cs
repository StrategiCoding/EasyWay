namespace EasyWay.Internals.BusinessRules
{
    internal sealed class BusinessRuleException : EasyWayException
    {
        internal BusinessRule BrokenBusinessRule { get; }

        internal BusinessRuleException(BusinessRule brokenBusinessRule)
            : base(brokenBusinessRule.Message)
        {
            BrokenBusinessRule = brokenBusinessRule;
        }

        public sealed override string ToString() => BrokenBusinessRule.ToString();
    }
}
