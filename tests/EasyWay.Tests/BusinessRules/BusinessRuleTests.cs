namespace EasyWay.Tests.BusinessRules
{
    public sealed class BusinessRuleTests
    {
        internal class TestBusinessRule : BusinessRule
        {
            public TestBusinessRule(string message) 
            {
                Message = message;
            }

            protected internal override string Message { get; }

            protected internal override bool IsFulfilled() => true;
        }

        [Fact(DisplayName = $"Method {nameof(BusinessRule.ToString)} for {nameof(BusinessRule)} should return the full type name and message")]
        public void BusinessRuleToStringTest()
        {
            // Arrange
            const string expectedMessage = "ExpectedMessage";

            string expectedToString = $"{typeof(TestBusinessRule).FullName}: {expectedMessage}";

            // Act
            var rule = new TestBusinessRule(expectedMessage);

            // Assert
            Assert.Equal(expectedToString, rule.ToString());
        }
    }
}
