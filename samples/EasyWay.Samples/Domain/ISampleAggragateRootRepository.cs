namespace EasyWay.Samples.Domain
{
    public interface ISampleAggragateRootRepository
    {
        public Task<SampleAggregateRoot?> Get(Guid id);

        public Task Add(SampleAggregateRoot aggregateRoot);
    }
}
