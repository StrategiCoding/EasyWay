using EasyWay.Events.DomainEvents;
using EasyWay.Internals.DomainEvents.AggragateRootIds;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.DomainEvents
{
    internal sealed class DomainEventPublisher : IDomainEventPublisher
    {
        private readonly IServiceProvider _serviceProvider;

        private readonly AggrageteRootIdSearcher _aggrageteRootIdSearcher;

        public DomainEventPublisher(
            IServiceProvider serviceProvider,
            AggrageteRootIdSearcher aggrageteRootIdSearcher)
        {
            _serviceProvider = serviceProvider;
            _aggrageteRootIdSearcher = aggrageteRootIdSearcher;
        }

        public async Task Publish(DomainEventContext domainEventContext)
        {
            var domainEvent = domainEventContext.DomainEvent;

            var handlerType = typeof(DomainEventHandler<>).MakeGenericType(domainEvent.GetType());

            var eventHandlers = _serviceProvider.GetServices(handlerType);

            var context = new Context(
                eventId: domainEventContext.EventId,
                aggragetRootId: _aggrageteRootIdSearcher.SearchId(domainEventContext.Entity),
                entityId: domainEventContext.Entity.Id,
                occurrenceOnUtc: domainEventContext.OccurrenceOnUtc);

            foreach (var eventHandler in eventHandlers)
            {
                await (Task)handlerType
                    .GetMethod(nameof(DomainEventHandler<DomainEvent>.Handle))?
                    .Invoke(eventHandler, new object[] { domainEvent, context });
            }
        }
    }
}
