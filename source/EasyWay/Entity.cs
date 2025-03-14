using EasyWay.Internals.BusinessRules;
using EasyWay.Internals.Clocks;
using EasyWay.Internals.DomainEvents;
using EasyWay.Internals.GuidGenerators;
using System.Diagnostics.CodeAnalysis;

namespace EasyWay
{
    public abstract class Entity : IEquatable<Entity>, IEqualityComparer<Entity>
    {
        internal Guid Id { get; private set; } = GuidGenerator.New;

        private List<DomainEventContext> _domainEvents = new List<DomainEventContext>();

        internal IReadOnlyCollection<DomainEventContext> DomainEvents => _domainEvents.AsReadOnly();

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

            var domainEventContext = new DomainEventContext()
            {
                EventId = GuidGenerator.New,
                AggragetRootId = Id, // TODO what when we have object graph
                EntityId = Id,
                OccurrenceOnUtc = InternalClock.UtcNow,
                DomainEvent = domainEvent,
            };

            _domainEvents.Add(domainEventContext);
        }

        public static bool operator ==(Entity x, Entity y) => EntityEquals(x, y);

        public static bool operator !=(Entity x, Entity y) => !EntityEquals(x, y);

        public bool Equals(Entity? x, Entity? y) => EntityEquals(x, y);

        public bool Equals(Entity? other) => EntityEquals(this, other);

        public sealed override bool Equals(object? obj) => obj is Entity entity ? EntityEquals(this, entity) : false;

        private static bool EntityEquals(Entity? x, Entity? y)
        {
            if (x is null && y is null) return true;
            if (x is null || y is null) return false;
            if (x.Id == y.Id && x.GetType() == y.GetType()) return true;

            return false;
        }

        public sealed override int GetHashCode() => GetHashCode(this);

        public int GetHashCode([DisallowNull] Entity obj) => HashCode.Combine(obj.Id, obj.GetType());
    }
}
