using EasyWay.Internals.GuidGenerators;

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
            EventId = GuidGenerator.New;
            OccurrenceOn = Clock.UtcNow;
        }
    }
}
