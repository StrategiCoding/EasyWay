using EasyWay.Samples.Domain;

namespace EasyWay.Samples.Events
{
    public sealed class SampleEventHandler2 : DomainEventHandler<CreatedSampleAggragete>
    {
        public sealed override Task Handle(CreatedSampleAggragete domainEvent)
        {
            return Task.CompletedTask;
        }
    }
}
