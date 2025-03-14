using EasyWay.Internals.DomainEvents;
using EasyWay.Tests.Entities.SeedWorks;

namespace EasyWay.Tests.Entities
{
    public sealed class AddDomainEventToEntity
    {
        internal sealed class TestDomainEvent : DomainEvent;

        [Fact(DisplayName = $"When a {nameof(DomainEvent)} is added, it should be in the list of {nameof(Entity.DomainEvents)}")]
        public void AddDomainEvent()
        {
            // Arrange
            var domainEvent = new TestDomainEvent();
            var entity = new TestEntity();

            // Act
            entity.AddDomainEvent(domainEvent);

            // Assert
            Assert.Equal(1, entity.DomainEvents.Count);
            Assert.Equal(domainEvent, entity.DomainEvents.Single().DomainEvent);
        }

        [Fact(DisplayName = $"When {nameof(DomainEvent)}s are added and then {nameof(Entity.ClearDomainEvents)} is executed, the domain event list should be empty")]
        public void ClearDomainEvents()
        {
            // Arrange
            var domainEvent = new TestDomainEvent();
            var entity = new TestEntity();

            entity.AddDomainEvent(domainEvent);

            // Act
            entity.ClearDomainEvents();

            // Assert
            Assert.Equal(0, entity.DomainEvents.Count);
        }

        [Fact(DisplayName =$"When a {nameof(DomainEvent)} is null {nameof(Entity)} should throw {nameof(DomainEventCannotBeNullException<DomainEvent>)}")]
        public void AddNullDomainEvent()
        {
            // Arrange
            TestDomainEvent? domainEvent = null;
            var entity = new TestEntity();

            // Assert
            var ex = Assert.Throws<DomainEventCannotBeNullException<TestDomainEvent>>(() =>
            {
                // Act
                entity.AddDomainEvent(domainEvent);
            });

            Assert.Equal($"{typeof(TestDomainEvent).Name} cannot be null", ex.Message);
        }
    }
}
