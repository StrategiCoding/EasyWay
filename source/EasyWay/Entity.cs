using EasyWay.Internals.DomainEvents;

namespace EasyWay
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        protected Entity() { }

        protected void CheckRule(BusinessRule businessRule)
        {
            if (!businessRule.IsFulfilled())
            {
                throw new BusinessRuleException(businessRule);
            }
        }

        public override bool Equals(object? obj)
        {
            if (obj is not null && obj is Entity entity) 
            { 
                return Id == entity.Id;
            }

            return false;
        }

        public static bool operator ==(Entity x, Entity y) => x.Id == y.Id;

        public static bool operator !=(Entity x, Entity y) => x.Id != y.Id;

        public override int GetHashCode() => Id.GetHashCode();
    }

    public abstract class Entity<TDomainEvent> : Entity
        where TDomainEvent : DomainEvent
    {
        private List<TDomainEvent> _domainEvents = new List<TDomainEvent>();

        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void ClearDomainEvents() => _domainEvents.Clear();

        protected void Add(TDomainEvent domainEvent)
        {
            if (domainEvent is null)
            {
                throw new DomainEventCannotBeNullException<TDomainEvent>();
            }

            _domainEvents.Add(domainEvent);
        }
    }
}
