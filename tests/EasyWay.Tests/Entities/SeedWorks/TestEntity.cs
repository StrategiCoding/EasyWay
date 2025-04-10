using static EasyWay.Tests.Entities.AddDomainEventToEntity;

namespace EasyWay.Tests.Entities.SeedWorks
{
    internal sealed class TestEntity : Entity
    {
        public void CheckBusinessRule(BusinessRule businessRule)
        {
            Check(businessRule);
        }

        public void AddDomainEvent<TDomainEvent>(TDomainEvent domainEvent)
            where TDomainEvent : DomainEvent
        {
            Apply(domainEvent);
        }

        private void When(TestDomainEvent @event) { }
    }
}
