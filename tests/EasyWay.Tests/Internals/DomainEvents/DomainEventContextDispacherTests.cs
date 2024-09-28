using EasyWay.Internals.DomainEvents;
using Moq;

namespace EasyWay.Tests.Internals.DomainEvents
{
    public sealed class DomainEventContextDispacherTests
    {
        internal sealed class TestDomainEvent : DomainEvent;

        private readonly Mock<IDomainEventBulkPublisher> _publisherMock = new Mock<IDomainEventBulkPublisher>();

        private readonly Mock<IDomainEventsContext> _contextMock = new Mock<IDomainEventsContext>();

        [Fact]
        public async Task WhenContextReturnsEmptyList()
        {
            // Arrange
            _contextMock
                .Setup(x => x.GetAllDomainEvents())
                .Returns(new List<DomainEvent>());

            // Act
            await new DomainEventContextDispacher(_publisherMock.Object, _contextMock.Object).Dispach();

            // Assert
            _contextMock.Verify(x => x.GetAllDomainEvents(), Times.Once);
            _contextMock.Verify(x => x.ClearAllDomainEvents(), Times.Never);
            _publisherMock.Verify(x => x.Publish(It.IsAny<IEnumerable<DomainEvent>>()), Times.Never);

            _contextMock.VerifyNoOtherCalls();
            _publisherMock.VerifyNoOtherCalls();
        }

        [Fact]
        public async Task WhenContextReturnsSeveralTimesListWithDomainEvent()
        {
            // Arrange
            var listWithDomainEvent = new List<DomainEvent>() { new TestDomainEvent() };
            var listWithoutDomainEvents = new List<DomainEvent>();

            _contextMock
                .SetupSequence(x => x.GetAllDomainEvents())
                .Returns(listWithDomainEvent)
                .Returns(listWithDomainEvent)
                .Returns(listWithDomainEvent)
                .Returns(listWithDomainEvent)
                .Returns(listWithDomainEvent)
                .Returns(listWithoutDomainEvents);

            // Act
            await new DomainEventContextDispacher(_publisherMock.Object, _contextMock.Object).Dispach();

            // Assert
            _contextMock.Verify(x => x.GetAllDomainEvents(), Times.Exactly(6));
            _contextMock.Verify(x => x.ClearAllDomainEvents(), Times.Exactly(5));
            _publisherMock.Verify(x => x.Publish(listWithDomainEvent), Times.Exactly(5));

            _contextMock.VerifyNoOtherCalls();
            _publisherMock.VerifyNoOtherCalls();
        }
    }
}
