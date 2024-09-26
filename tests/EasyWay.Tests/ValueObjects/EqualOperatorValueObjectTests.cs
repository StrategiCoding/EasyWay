using EasyWay.Tests.ValueObjects.SeedWork;

namespace EasyWay.Tests.ValueObjects
{
    public sealed class EqualOperatorValueObjectTests
    {
        const string VALUE = "TEST";
        const int INT_VALUE = 10;

        [Fact]
        public void EqualOperator()
        {
            var x1 = TestValueObject.Create(VALUE, INT_VALUE);
            var x2 = TestValueObject.Create(VALUE, INT_VALUE);

            Assert.True(x1 == x2);
        }

        [Fact]
        public void EqualOperatorWithNull()
        {
            var x1 = TestValueObject.Create(VALUE, INT_VALUE);

            Assert.False(x1 == null);
            Assert.False(null == x1);
        }

        [Fact]
        public void EqualOperatorWithDiffType()
        {
            var x1 = TestValueObject.Create(VALUE, INT_VALUE);
            var y1 = TestValueObject2.Create(VALUE, INT_VALUE);

            Assert.False(x1 == y1);
        }
    }
}
