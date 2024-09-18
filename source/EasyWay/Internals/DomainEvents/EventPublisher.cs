using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.DomainEvents
{
    internal sealed class EventPublisher : IEventPublisher
    {
        private readonly IServiceProvider _serviceProvider;

        public EventPublisher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Publish<TEvent>(TEvent @event)
            where TEvent : DomainEvent
        {
            var eventHandlers = _serviceProvider.GetServices<IEventHandler<TEvent>>();

            foreach (var eventHandler in eventHandlers)
            {
                await eventHandler.Handle(@event).ConfigureAwait(false);
            }
        }
    }
}
