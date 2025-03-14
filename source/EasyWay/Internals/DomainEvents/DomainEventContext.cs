namespace EasyWay.Internals.DomainEvents
{
    internal sealed class DomainEventContext
    {
        internal required Guid EventId { get; init; }

        internal required Guid AggragetRootId { get; init; }

        internal required Guid EntityId { get; init; }

        internal required DateTime OccurrenceOnUtc { get; init; }

        internal required DomainEvent DomainEvent { get; init; }
    }
}
