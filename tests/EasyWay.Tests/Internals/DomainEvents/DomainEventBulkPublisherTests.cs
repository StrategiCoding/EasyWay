using EasyWay.Internals.DomainEvents;
using Moq;

namespace EasyWay.Tests.Internals.DomainEvents
{
    public sealed class DomainEventBulkPublisherTests
    {
        internal sealed class TestDomainEvent : DomainEvent;

        private readonly Mock<IDomainEventPublisher> _domainEventPublisherMock = new Mock<IDomainEventPublisher>();

        [Theory]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(10)]
        public async Task DomainEventBulkPublisherShouldUsePublisherToPublishListOfDomainEvents(int numberOfDomainEvents)
        {
            // Arrange
            _domainEventPublisherMock
                .Setup(x => x.Publish(It.IsAny<DomainEvent>()))
                .Returns(Task.CompletedTask);

            List<DomainEvent> events = new List<DomainEvent>();

            for(int i = 0; i < numberOfDomainEvents; i++) 
            {
                events.Add(new TestDomainEvent());
            }

            // Act
            await new DomainEventBulkPublisher(_domainEventPublisherMock.Object).Publish(events);

            // Assert
            _domainEventPublisherMock.Verify(x => x.Publish(It.IsAny<DomainEvent>()), Times.Exactly(numberOfDomainEvents));
            _domainEventPublisherMock.VerifyNoOtherCalls();
        }
    }
}
