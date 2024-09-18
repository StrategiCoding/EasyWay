namespace EasyWay.Samples
{
    public class SampleEvent : IEvent
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}
