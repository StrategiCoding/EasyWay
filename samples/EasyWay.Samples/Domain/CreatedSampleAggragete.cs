namespace EasyWay.Samples.Domain
{
    public sealed class CreatedSampleAggragete : DomainEvent
    {
        public string test { get; } = "TEST";
    }
}
