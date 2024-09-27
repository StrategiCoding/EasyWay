using EasyWay.Tests.ValueObjects.SeedWork;

namespace EasyWay.Tests.ValueObjects
{
    public class NotEqualOperatorValueObjectTests
    {
        const string VALUE = "TEST";
        const int INT_VALUE = 10;

        [Fact]
        public void NotEqualOperator()
        {
            var x1 = TestValueObject.Create(VALUE, INT_VALUE);
            var x2 = TestValueObject.Create(VALUE, INT_VALUE + 1);

            Assert.True(x1 != x2);
            Assert.True(x2 != x1);
        }

        [Fact]
        public void NotEqualOperatorWithNull()
        {
            var x1 = TestValueObject.Create(VALUE, INT_VALUE);

            Assert.True(x1 != null);
            Assert.True(null != x1);
        }

        [Fact]
        public void NotEqualOperatorWithDiffType()
        {
            var x1 = TestValueObject.Create(VALUE, INT_VALUE);
            var y1 = TestValueObject2.Create(VALUE, INT_VALUE);

            Assert.True(x1 != y1);
            Assert.True(y1 != x1);
        }
    }
}
