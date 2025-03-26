using EasyWay.Samples.Domain;

namespace EasyWay.Samples.Databases
{
    public class SampleAggragateRootRepository : ISampleAggragateRootRepository, IAsyncDisposable
    {
        private readonly IWriteGenericRepository<SampleAggregateRoot> _repository;

        public SampleAggragateRootRepository(IWriteGenericRepository<SampleAggregateRoot> repository)
        {
            _repository = repository;
        }

        public Task Add(SampleAggregateRoot aggregateRoot)
        {
            return _repository.Add(aggregateRoot);
        }

        public ValueTask DisposeAsync()
        {
            return ValueTask.CompletedTask;
        }

        public Task<SampleAggregateRoot?> Get(Guid id)
        {
            return _repository.Get(id);
        }
    }
}
