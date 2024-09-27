using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace EasyWay
{
    public abstract class ValueObject : IEquatable<ValueObject>, IEqualityComparer<ValueObject>
    {
        private static readonly BindingFlags _bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public;

        internal IEnumerable<PropertyInfo> Properties => GetProperties();

        internal IEnumerable<FieldInfo> Fields => GetFields();

        private List<PropertyInfo> _properties;

        private List<FieldInfo> _fields;

        public static bool operator ==(ValueObject obj1, ValueObject obj2) => ValueObjectEquals(obj1, obj2);

        public static bool operator !=(ValueObject obj1, ValueObject obj2) => !ValueObjectEquals(obj1, obj2);

        public bool Equals(ValueObject? obj) => ValueObjectEquals(this, obj);

        public bool Equals(ValueObject? x, ValueObject? y) => ValueObjectEquals(x, y);

        public sealed override bool Equals(object? obj) => obj is ValueObject valueObject ? ValueObjectEquals(this, valueObject) : false;

        public int GetHashCode([DisallowNull] ValueObject obj) => obj.GetHashCode();

        public sealed override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;

                hash = HashValue(hash, GetType());

                foreach (var prop in GetProperties())
                {
                    var value = prop.GetValue(this, null);
                    hash = HashValue(hash, value);
                }

                foreach (var field in GetFields())
                {
                    var value = field.GetValue(this);
                    hash = HashValue(hash, value);
                }

                return hash;
            }
        }

        private IEnumerable<PropertyInfo> GetProperties()
        {
            if (_properties == null)
            {
                var properties = GetType()
                    .GetProperties(_bindingFlags)
                    .ToList();

                var p = properties.Single(x => x.Name == nameof(Properties));
                var f = properties.Single(x => x.Name == nameof(Fields));

                properties.Remove(p);
                properties.Remove(f);

                _properties = properties;
            }

            return _properties;
        }

        private IEnumerable<FieldInfo> GetFields()
        {
            if (_fields == null)
            {
                _fields = GetType()
                    .GetFields(_bindingFlags)
                    .ToList();
            }

            return _fields;
        }

        private int HashValue(int seed, object value)
        {
            var currentHash = value?.GetHashCode() ?? 0;

            return (seed * 23) + currentHash;
        }

        private static bool ValueObjectEquals(ValueObject? x, ValueObject? y)
        {
            if (x is null && y is null) return true;
            if (x is null || y is null) return false;
            if (x.GetType() == y.GetType())
            {
                return x.Properties.All(propertyInfo => PropertiesAreEqual(x, y, propertyInfo))
                       &&
                       x.Fields.All(fieldInfo => FieldsAreEqual(x, y, fieldInfo));
            }

            return false;
        }

        private static bool PropertiesAreEqual(ValueObject x, ValueObject y, PropertyInfo p) => Equals(p.GetValue(x), p.GetValue(y));

        private static bool FieldsAreEqual(ValueObject x, ValueObject y, FieldInfo f) => Equals(f.GetValue(x), f.GetValue(y));
    }
}
