using EasyWay.Samples.Domain;

namespace EasyWay.Samples.Events
{
    public class SampleEventHandler2 : IDomainEventHandler<CreatedSampleAggragete>
    {
        public Task Handle(CreatedSampleAggragete @event)
        {
            return Task.CompletedTask;
        }
    }
}
