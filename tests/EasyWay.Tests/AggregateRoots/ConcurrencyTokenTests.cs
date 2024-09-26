namespace EasyWay.Tests.AggregateRoots
{
    public sealed class ConcurrencyTokenTests
    {
        internal sealed class TestAggragateRoot : AggregateRoot;

        [Fact(DisplayName = $"{nameof(AggregateRoot.ConcurrencyToken)} should be 0 when {nameof(AggregateRoot)} is created")]
        public void ConcurrencyTokenDefaultValueTest()
        {
            Assert.Equal(0, new TestAggragateRoot().ConcurrencyToken);
        }

        [Fact(DisplayName = $"{nameof(AggregateRoot.ConcurrencyToken)} should be incremented after {nameof(AggregateRoot.UpdateConcurrencyToken)} is executed")]
        public void UpdateConcurrencyTokenTest()
        {
            // Arrange
            var aggragateRoot = new TestAggragateRoot();

            // Act
            aggragateRoot.UpdateConcurrencyToken();

            // Assert
            Assert.Equal(1, aggragateRoot.ConcurrencyToken);
        }

        [Fact(DisplayName = $"When {nameof(AggregateRoot.ConcurrencyToken)} has the maximum value (32767) after the update it should start from the minimum value (-32768)")]
        public void UpdateMaxConcurrencyTokenValueTest()
        {
            const short maxValue = short.MaxValue;
            const short minValue = short.MinValue;

            // Arrange
            var aggragateRoot = new TestAggragateRoot();

            for (var i = 0; i < maxValue; i++) 
            {
                aggragateRoot.UpdateConcurrencyToken();
            }

            // Act
            aggragateRoot.UpdateConcurrencyToken();

            // Assert
            Assert.Equal(minValue, aggragateRoot.ConcurrencyToken);
        }
    }
}
