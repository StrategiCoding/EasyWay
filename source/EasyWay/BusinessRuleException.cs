using EasyWay.Internals;

namespace EasyWay
{
    public sealed class BusinessRuleException : EasyWayException
    {
        public BusinessRule BrokenBusinessRule { get; }

        internal BusinessRuleException(BusinessRule brokenBusinessRule) 
            : base(brokenBusinessRule.Message)
        {
            BrokenBusinessRule = brokenBusinessRule;
        }

        public override string ToString()
        {
            return $"{BrokenBusinessRule.GetType().FullName}: {BrokenBusinessRule.Message}";
        }
    }
}
