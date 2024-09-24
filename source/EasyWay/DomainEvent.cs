using EasyWay.Internals.IdGenerators;

namespace EasyWay
{
    /// <summary>
    /// Represents an event
    /// </summary>
    public abstract class DomainEvent
    {
        internal Guid EventId { get; }

        internal DateTime OccurrenceOn { get; }

        protected DomainEvent()
        {
            EventId = IdGenerator.New;
            OccurrenceOn = Clock.UtcNow;
        }
    }
}
