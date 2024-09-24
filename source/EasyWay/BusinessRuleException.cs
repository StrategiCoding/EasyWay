namespace EasyWay
{
    public sealed class BusinessRuleException : EasyWayException
    {
        internal BusinessRule BrokenBusinessRule { get; }

        internal BusinessRuleException(BusinessRule brokenBusinessRule) 
            : base(brokenBusinessRule.Message)
        {
            BrokenBusinessRule = brokenBusinessRule;
        }

        public sealed override string ToString()
        {
            return $"{BrokenBusinessRule.GetType().FullName}: {BrokenBusinessRule.Message}";
        }
    }
}
