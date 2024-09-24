using EasyWay.Internals.DomainEvents;
using EasyWay.Internals.IdGenerators;

namespace EasyWay
{
    public abstract class Entity
    {
        internal Guid Id { get; private set; } = IdGenerator.New;

        private List<DomainEvent> _domainEvents = new List<DomainEvent>();

        internal IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        internal void ClearDomainEvents() => _domainEvents.Clear();

        protected Entity() { }

        protected static void CheckRule(BusinessRule businessRule)
        {
            if (!businessRule.IsFulfilled())
            {
                throw new BusinessRuleException(businessRule);
            }
        }

        protected void Add<TDomainEvent>(TDomainEvent domainEvent)
            where TDomainEvent : DomainEvent
        {
            if (domainEvent is null)
            {
                throw new DomainEventCannotBeNullException<TDomainEvent>();
            }

            _domainEvents.Add(domainEvent);
        }

        public sealed override bool Equals(object? obj)
        {
            if (obj is not null && obj is Entity entity) 
            { 
                return Id == entity.Id;
            }

            return false;
        }

        public static bool operator ==(Entity x, Entity y) => x.Id == y.Id;

        public static bool operator !=(Entity x, Entity y) => x.Id != y.Id;

        public sealed override int GetHashCode() => Id.GetHashCode();
    }
}
