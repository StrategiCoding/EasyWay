
using EasyWay.Samples.Domain;

namespace EasyWay.Samples
{
    public class SampleEventHandler1 : IDomainEventHandler<CreatedSampleAggragete>
    {
        public Task Handle(CreatedSampleAggragete @event)
        {
            return Task.CompletedTask;
        }
    }
}
