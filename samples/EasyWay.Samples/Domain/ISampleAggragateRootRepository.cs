namespace EasyWay.Samples.Domain
{
    public interface ISampleAggragateRootRepository : IRepository
    {
        public Task<SampleAggregateRoot?> Get(Guid id);

        public Task Add(SampleAggregateRoot aggregateRoot);
    }
}
