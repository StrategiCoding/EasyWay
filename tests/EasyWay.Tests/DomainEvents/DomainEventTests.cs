using EasyWay.Internals.Clocks;
using Microsoft.Extensions.Time.Testing;

namespace EasyWay.Tests.DomainEvents
{
    public sealed class DomainEventTests
    {
        internal sealed class TestDomainEvent : DomainEvent;

        [Fact(DisplayName = $"{nameof(DomainEvent)} should create with correct {nameof(DomainEvent.EventId)} and {nameof(DomainEvent.OccurrenceOn)}")]
        public void Test()
        {
            // Arrange
            var expectedDateTime = DateTime.UtcNow.AddMonths(-6);
            var precision = TimeSpan.FromMilliseconds(50);

            InternalClock.Test(new FakeTimeProvider(expectedDateTime));

            // Act
            var domainEvent = new TestDomainEvent();

            // Assert
            Assert.NotEqual(Guid.Empty, domainEvent.EventId);
            Assert.Equal(expectedDateTime, domainEvent.OccurrenceOn, precision);
        }
    }
}
