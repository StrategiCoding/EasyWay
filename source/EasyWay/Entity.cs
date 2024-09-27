using EasyWay.Internals.BusinessRules;
using EasyWay.Internals.DomainEvents;
using EasyWay.Internals.IdGenerators;
using System.Diagnostics.CodeAnalysis;

namespace EasyWay
{
    public abstract class Entity : IEquatable<Entity>, IEqualityComparer<Entity>
    {
        internal Guid Id { get; private set; } = IdGenerator.New;

        private List<DomainEvent> _domainEvents = new List<DomainEvent>();

        internal IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        internal void ClearDomainEvents() => _domainEvents.Clear();

        protected Entity() { }

        protected static void Check(BusinessRule businessRule)
        {
            if (!businessRule.IsFulfilled())
            {
                throw new BrokenBusinessRuleException(businessRule);
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

        public bool Equals(Entity? other) => other is not null ? Id == other.Id : false;

        public sealed override bool Equals(object? obj) => obj is Entity entity ? Equals(entity) : false;

        public static bool operator ==(Entity x, Entity y) => x.Id == y.Id;

        public static bool operator !=(Entity x, Entity y) => x.Id != y.Id;

        public sealed override int GetHashCode() => Id.GetHashCode();

        public bool Equals(Entity? x, Entity? y)
        {
            if (x is null && y is null) return true;
            if (x is null || y is null) return false;
            if (x.Id == y.Id) return true;

            return false;
        }

        public int GetHashCode([DisallowNull] Entity obj) => obj.Id.GetHashCode();
    }
}
