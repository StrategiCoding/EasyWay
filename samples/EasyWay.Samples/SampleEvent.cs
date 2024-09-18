namespace EasyWay.Samples
{
    public class SampleEvent : DomainEvent
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}
