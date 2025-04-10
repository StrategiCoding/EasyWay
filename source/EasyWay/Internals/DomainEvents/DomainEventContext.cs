namespace EasyWay.Internals.DomainEvents
{
    internal sealed class DomainEventContext
    {
        internal required Guid EventId { get; init; }

        internal required Entity Entity { get; init; }

        internal required DateTime OccurrenceOnUtc { get; init; }

        internal required DomainEvent DomainEvent { get; init; }
    }
}
