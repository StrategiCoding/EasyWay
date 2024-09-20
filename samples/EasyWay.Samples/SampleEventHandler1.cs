
namespace EasyWay.Samples
{
    public class SampleEventHandler1 : IDomainEventHandler<SampleEvent>
    {
        public Task Handle(SampleEvent @event)
        {
            return Task.CompletedTask;
        }
    }
}
