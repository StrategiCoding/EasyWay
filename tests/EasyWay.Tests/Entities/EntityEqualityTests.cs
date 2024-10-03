using EasyWay.Internals.GuidGenerators;

namespace EasyWay.Tests.Entities
{
    public class EntityEqualityTests
    {
        internal sealed class X : Entity;

        internal sealed class Y : Entity;

        [Fact]
        public void EntitiesAreNull()
        {
            var b = new X();

            X x1 = null;
            X x2 = null;

            Assert.True(b.Equals(x1,x2));
            Assert.True(b.Equals(x2, x1));

            Assert.True(null as Entity == null as Entity);
            Assert.False(null as Entity != null as Entity);
        }

        [Fact]
        public void WhenOneEntityIsNull()
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
        public void EntitiesWithTheSameIdAndTypeAreEqual()
        {
            var guid = new Guid("0fe4961d-85f4-4340-8e49-5438800919ef");

            GuidGenerator.Set(guid);

            var x1 = new X();
            var x2 = new X();

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

            Assert.True(x1.GetHashCode() != guid.GetHashCode());
            Assert.True(x2.GetHashCode() != guid.GetHashCode());

            GuidGenerator.Reset();
        }

        [Fact]
        public void EntitiesWithTheSameIdAndDiffTypeAreNotEqual()
        {
            var guid = new Guid("0fe4961d-85f4-4340-8e49-5438800919ef");

            GuidGenerator.Set(guid);

            var x = new X();
            var y = new Y();

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

            Assert.True(x.GetHashCode() != guid.GetHashCode());
            Assert.True(y.GetHashCode() != guid.GetHashCode());

            GuidGenerator.Reset();
        }

        [Fact]
        public void EntitiesWithDiffIdAndTheSameTypeAreNotEqual()
        {
            GuidGenerator.Set(new Guid("0fe4961d-85f4-4340-8e49-5438800919ef"));

            var x1 = new X();

            GuidGenerator.Set(new Guid("0ae4961d-85f4-0000-8e49-5438800919ef"));

            var x2 = new X();

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

            GuidGenerator.Reset();
        }

        [Fact]
        public void EntitiesWithDiffIdAndDiffTypeAreNotEqual()
        {
            GuidGenerator.Set(new Guid("0fe4961d-85f4-4340-8e49-5438800919ef"));

            var x = new X();

            GuidGenerator.Set(new Guid("0ae4961d-85f4-0000-8e49-5438800919ef"));

            var y = new Y();

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

            GuidGenerator.Reset();
        }
    }
}
