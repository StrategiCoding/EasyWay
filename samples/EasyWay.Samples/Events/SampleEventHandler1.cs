using EasyWay.Events.DomainEvents;
using EasyWay.Samples.Domain;

namespace EasyWay.Samples.Events
{
    public sealed class SampleEventHandler1 : DomainEventHandler<CreatedSampleAggragete>
    {
        public override Task Handle(CreatedSampleAggragete domainEvent, Context context)
        {
            return Task.CompletedTask;
        }
    }
}
