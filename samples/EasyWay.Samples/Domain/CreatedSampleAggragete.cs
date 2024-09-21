namespace EasyWay.Samples.Domain
{
    public class CreatedSampleAggragete : DomainEvent
    {
        public Guid Id { get; } = Guid.NewGuid();
    }
}
