namespace EasyWay.Tests.ValueObjects.SeedWork
{
    internal sealed class TestValueObject2 : ValueObject
    {
        public string StringValue { get; }

        private int _privateIntValue { get; }

        private TestValueObject2(string value, int intValue)
        {
            StringValue = value;
            _privateIntValue = intValue;
        }

        public static TestValueObject2 Create(string value, int intValue) => new TestValueObject2(value, intValue);
    }
}
