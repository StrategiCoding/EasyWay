
namespace EasyWay.Samples
{
    public class SampleEventHandler2 : IDomainEventHandler<SampleEvent>
    {
        public Task Handle(SampleEvent @event)
        {
            return Task.CompletedTask;
        }
    }
}
