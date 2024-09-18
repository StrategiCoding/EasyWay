namespace EasyWay.Samples
{
    public class SampleEvent : Event
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}
