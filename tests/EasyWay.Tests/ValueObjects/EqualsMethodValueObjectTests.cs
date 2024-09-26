using EasyWay.Tests.ValueObjects.SeedWork;

namespace EasyWay.Tests.ValueObjects
{
    public class EqualsMethodValueObjectTests
    {
        const string VALUE = "TEST";
        const int INT_VALUE = 10;

        [Fact]
        public void EqualsMethod()
        {
            object x1 = TestValueObject.Create(VALUE, INT_VALUE);
            object x2 = TestValueObject.Create(VALUE, INT_VALUE);

            Assert.True(x1.Equals(x2));
        }

        [Fact]
        public void EqualsMethodWithNull()
        {
            object x1 = TestValueObject.Create(VALUE, INT_VALUE);

            Assert.False(x1.Equals(null));
        }

        [Fact]
        public void EqualsMethodWithDiffType()
        {
            object x1 = TestValueObject.Create(VALUE, INT_VALUE);
            object y1 = TestValueObject2.Create(VALUE, INT_VALUE);

            Assert.False(x1.Equals(y1));
        }

        [Fact]
        public void EqualsMethodWithDiffValue()
        {
            object x1 = TestValueObject.Create(VALUE, INT_VALUE);
            object x2 = TestValueObject.Create(VALUE, INT_VALUE + 1);

            Assert.False(x1.Equals(x2));
        }
    }
}
