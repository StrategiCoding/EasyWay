namespace EasyWay.Samples.Domain
{
    public sealed class SampleAggrageteId : AggregateRootId
    {
        public Guid Id { get; }

        public SampleAggrageteId(Guid id)
        {
            Id = id;
        }
    }
}
