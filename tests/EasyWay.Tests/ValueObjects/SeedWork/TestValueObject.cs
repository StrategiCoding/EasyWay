namespace EasyWay.Tests.ValueObjects.SeedWork
{
    internal sealed class TestValueObject : ValueObject
    {
        public string StringValue { get; }

        private int _privateIntValue { get; }

        private TestValueObject(string value, int intValue)
        {
            StringValue = value;
            _privateIntValue = intValue;
        }

        public static TestValueObject Create(string value, int intValue) => new TestValueObject(value, intValue);
    }
}
