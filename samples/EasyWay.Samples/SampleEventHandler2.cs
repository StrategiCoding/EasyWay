
namespace EasyWay.Samples
{
    public class SampleEventHandler2 : IEventHandler<SampleEvent>
    {
        public Task Handle(SampleEvent @event)
        {
            return Task.CompletedTask;
        }
    }
}
