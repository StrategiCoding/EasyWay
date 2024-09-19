using EasyWay.Internals.DomainEvents;

namespace EasyWay
{
    public abstract class Entity<TEntityId>
        where TEntityId : EntityId
    {
        public TEntityId Id { get; protected set; }

        protected Entity() { }

        protected void CheckRule(BusinessRule businessRule)
        {
            if (!businessRule.IsFulfilled())
            {
                throw new BusinessRuleException(businessRule);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            return Id == obj as TEntityId;
        }

        public static bool operator ==(Entity<TEntityId> x, Entity<TEntityId> y) => x.Equals(y);

        public static bool operator !=(Entity<TEntityId> x, Entity<TEntityId> y) => !x.Equals(y);

        public override int GetHashCode() => Id.GetHashCode();
    }

    public abstract class Entity<TEntityId, TDomainEvent> : Entity<TEntityId>
        where TEntityId : EntityId
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
