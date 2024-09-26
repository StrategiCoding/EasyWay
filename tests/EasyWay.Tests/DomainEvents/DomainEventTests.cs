namespace EasyWay.Tests.DomainEvents
{
    public sealed class DomainEventTests
    {
        internal sealed class TestDomainEvent : DomainEvent;

        [Fact(DisplayName = $"Domain event should create with correct '{nameof(DomainEvent.EventId)}' and '{nameof(DomainEvent.OccurrenceOn)}'")]
        public void Test()
        {
            // Arrange
            var expectedDateTime = DateTime.UtcNow;
            var precision = TimeSpan.FromMilliseconds(50);

            Clock.Set(expectedDateTime);

            // Act
            var domainEvent = new TestDomainEvent();

            // Assert
            Assert.NotEqual(Guid.Empty, domainEvent.EventId);
            Assert.Equal(expectedDateTime, domainEvent.OccurrenceOn, precision);
        }
    }
}
