using EasyWay.Tests.ValueObjects.SeedWork;

namespace EasyWay.Tests.ValueObjects
{
    public sealed class ValueObjectEqualityTests
    {
        const string VALUE = "TEST";
        const int INT_VALUE = 10;

        internal sealed class X : ValueObject;

        [Fact]
        public void ValueObjectsAreNull()
        {
            var b = new X();

            X x1 = null;
            X x2 = null;

            Assert.True(b.Equals(x1, x2));
            Assert.True(b.Equals(x2, x1));

            Assert.True(null as ValueObject == null as ValueObject);
            Assert.False(null as ValueObject != null as ValueObject);
        }

        [Fact]
        public void WhenOneValueObjectIsNull()
        {
            X x = new X();

            Assert.False(x == null);
            Assert.False(null == x);

            Assert.True(x != null);
            Assert.True(null != x);

            Assert.False(x.Equals(x, null));
            Assert.False(x.Equals(null, x));
            Assert.False(x.Equals(null));
            Assert.False(x.Equals(null as object));
        }

        [Fact]
        public void ValueObjectsWithTheSameIdAndTypeAreEqual()
        {
            var x1 = TestValueObject.Create(VALUE, INT_VALUE);
            var x2 = TestValueObject.Create(VALUE, INT_VALUE);

            Assert.True(x1 == x2);
            Assert.True(x2 == x1);

            Assert.False(x1 != x2);
            Assert.False(x2 != x1);

            Assert.True(x1.Equals(x2));
            Assert.True(x2.Equals(x1));

            Assert.True(x1.Equals(x2 as object));
            Assert.True(x2.Equals(x1 as object));

            Assert.True(x1.Equals(x1, x2));
            Assert.True(x2.Equals(x2, x1));

            Assert.True(x1.GetHashCode() == x2.GetHashCode());
            Assert.True(x1.GetHashCode(x1) == x2.GetHashCode(x2));
        }

        [Fact]
        public void ValueObjectsWithTheValuesAndDiffTypeAreNotEqual()
        {
            var x = TestValueObject.Create(VALUE, INT_VALUE);
            var y = TestValueObject2.Create(VALUE, INT_VALUE);

            Assert.False(x == y);
            Assert.False(y == x);

            Assert.True(x != y);
            Assert.True(y != x);

            Assert.False(x.Equals(y));
            Assert.False(y.Equals(x));

            Assert.False(x.Equals(y as object));
            Assert.False(y.Equals(x as object));

            Assert.False(x.Equals(x, y));
            Assert.False(y.Equals(y, x));

            Assert.True(x.GetHashCode() != y.GetHashCode());
            Assert.True(x.GetHashCode(x) != y.GetHashCode(y));
        }

        [Fact]
        public void ValueObjectsWithDiffValuesAndTheSameTypeAreNotEqual()
        {
            var x1 = TestValueObject.Create(VALUE, INT_VALUE);
            var x2 = TestValueObject.Create(VALUE, INT_VALUE + 1);

            Assert.False(x1 == x2);
            Assert.False(x2 == x1);

            Assert.True(x1 != x2);
            Assert.True(x2 != x1);

            Assert.False(x1.Equals(x2));
            Assert.False(x2.Equals(x1));

            Assert.False(x1.Equals(x2 as object));
            Assert.False(x2.Equals(x1 as object));

            Assert.False(x1.Equals(x1, x2));
            Assert.False(x2.Equals(x2, x1));

            Assert.True(x1.GetHashCode() != x2.GetHashCode());
        }

        [Fact]
        public void ValueObjectsWithDiffValuesAndDiffTypeAreNotEqual()
        {
            var x = TestValueObject.Create(VALUE, INT_VALUE);
            var y = TestValueObject2.Create(VALUE, INT_VALUE);

            Assert.False(x == y);
            Assert.False(y == x);

            Assert.True(x != y);
            Assert.True(y != x);

            Assert.False(x.Equals(y));
            Assert.False(y.Equals(x));

            Assert.False(x.Equals(y as object));
            Assert.False(y.Equals(x as object));

            Assert.False(x.Equals(x, y));
            Assert.False(y.Equals(y, x));

            Assert.True(x.GetHashCode() != y.GetHashCode());
        }
    }
}
