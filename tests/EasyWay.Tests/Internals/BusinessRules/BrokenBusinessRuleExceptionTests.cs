using EasyWay.Internals.BusinessRules;

namespace EasyWay.Tests.Internals.BusinessRules
{
    public sealed class BrokenBusinessRuleExceptionTests
    {
        internal sealed class TestBusinessRule : BusinessRule
        {
            public TestBusinessRule(string message) => Message = message;

            protected internal override string Message { get; }

            protected internal override bool IsFulfilled() => false;
        }

        [Fact(DisplayName = $"{nameof(BrokenBusinessRuleException)} should correctly create based on a broken {nameof(BusinessRule)}")]
        public void Test()
        {
            var brokenBussinesRule = new TestBusinessRule("Message");

            var exception = new BrokenBusinessRuleException(brokenBussinesRule);

            Assert.Equal(brokenBussinesRule, exception.BrokenBusinessRule);
            Assert.Equal(brokenBussinesRule.ToString(), exception.ToString());
            Assert.Equal(brokenBussinesRule.Message, exception.Message);
        }
    }
}
