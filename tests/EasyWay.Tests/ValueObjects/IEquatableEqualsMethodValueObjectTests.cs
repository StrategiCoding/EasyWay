using EasyWay.Tests.ValueObjects.SeedWork;

namespace EasyWay.Tests.ValueObjects
{
    public sealed class IEquatableEqualsMethodValueObjectTests
    {
        const string VALUE = "TEST";
        const int INT_VALUE = 10;

        [Fact]
        public void EqualsMethod()
        {
            var x1 = TestValueObject.Create(VALUE, INT_VALUE);
            var x2 = TestValueObject.Create(VALUE, INT_VALUE);

            Assert.True(x1.Equals(x2));
        }

        [Fact]
        public void EqualsMethodWithNull()
        {
            var x1 = TestValueObject.Create(VALUE, INT_VALUE);

            Assert.False(x1.Equals(null));
        }

        [Fact]
        public void EqualsMethodWithDiffType()
        {
            var x1 = TestValueObject.Create(VALUE, INT_VALUE);
            var y1 = TestValueObject2.Create(VALUE, INT_VALUE);

            Assert.False(x1.Equals(y1));
        }

        [Fact]
        public void EqualsMethodWithDiffValue()
        {
            var x1 = TestValueObject.Create(VALUE, INT_VALUE);
            var x2 = TestValueObject.Create(VALUE, INT_VALUE + 1);

            Assert.False(x1.Equals(x2));
        }
    }
}
