using EasyWay.Internals.BusinessRules;
using EasyWay.Tests.Entities.SeedWorks;
using EasyWay.Tests.Entities.SeedWorks.BusinessRules;

namespace EasyWay.Tests.Entities
{
    public sealed class CheckBusinessRuleEntityTests
    {
        [Fact(DisplayName =$"When {nameof(BusinessRule)} is broken, the {nameof(Entity)} should throw a {nameof(BrokenBusinessRuleException)}")]
        public void WhenBusinessRuleIsBrokenEntityShouldThrowBrokenBusinessRuleException()
        {
            // Arrange
            var entity = new TestEntity();
            var brokenBusinessRule = new TestBrokenBusinessRule();

            // Act
            var act = () => entity.CheckBusinessRule(brokenBusinessRule);

            // Assert
            var ex = Assert.Throws<BrokenBusinessRuleException>(act);

            Assert.Equal(brokenBusinessRule, ex.BrokenBusinessRule);
        }

        [Fact(DisplayName = $"When {nameof(BusinessRule)} is not broken, the {nameof(Entity)} should not throw any {nameof(Exception)}")]
        public void WhenBusinessRuleIsNotBrokenEntityShouldNotThrowAnyException()
        {
            // Arrange
            var entity = new TestEntity();
            var notBrokenBusinessRule = new TestNotBrokenBusinessRule();

            // Act & Assert
            entity.CheckBusinessRule(notBrokenBusinessRule);
        }
    }
}
