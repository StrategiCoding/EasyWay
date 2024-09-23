using EasyWay.Internals.IdGenerators;

namespace EasyWay
{
    /// <summary>
    /// Represents an event
    /// </summary>
    public abstract class DomainEvent
    {
        public Guid EventId { get; }

        public DateTime OccurrenceOn { get; }

        protected DomainEvent()
        {
            EventId = IdGenerator.New;
            OccurrenceOn = Clock.UtcNow;
        }
    }
}
