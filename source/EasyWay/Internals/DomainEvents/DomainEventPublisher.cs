using EasyWay.Events.DomainEvents;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.DomainEvents
{
    internal sealed class DomainEventPublisher : IDomainEventPublisher
    {
        private readonly IServiceProvider _serviceProvider;

        public DomainEventPublisher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Publish(DomainEventContext domainEventContext)
        {
            var domainEvent = domainEventContext.DomainEvent;

            var handlerType = typeof(DomainEventHandler<>).MakeGenericType(domainEvent.GetType());

            var eventHandlers = _serviceProvider.GetServices(handlerType);

            var context = new Context(
                eventId: domainEventContext.EntityId,
                aggragetRootId: domainEventContext.AggragetRootId,
                entityId: domainEventContext.EntityId,
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
