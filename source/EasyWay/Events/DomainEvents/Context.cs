namespace EasyWay.Events.DomainEvents
{
    public sealed class Context
    {
        public Guid EventId { get; }

        public Guid AggragetRootId { get; }

        public Guid EntityId { get; }

        public DateTime OccurrenceOnUtc { get; }

        internal Context(
            Guid eventId,
            Guid aggragetRootId,
            Guid entityId,
            DateTime occurrenceOnUtc)
        {
            EventId = eventId;
            AggragetRootId = aggragetRootId;
            EntityId = entityId;
            OccurrenceOnUtc = occurrenceOnUtc;
        }
    }
}
