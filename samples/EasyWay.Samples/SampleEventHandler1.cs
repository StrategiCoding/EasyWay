
namespace EasyWay.Samples
{
    public class SampleEventHandler1 : IEventHandler<SampleEvent>
    {
        public Task Handle(SampleEvent @event)
        {
            return Task.CompletedTask;
        }
    }
}
