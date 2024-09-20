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

        public async Task Publish<TEvent>(TEvent @event)
            where TEvent : DomainEvent
        {
            var eventHandlers = _serviceProvider.GetServices<IDomainEventHandler<TEvent>>();

            foreach (var eventHandler in eventHandlers)
            {
                await eventHandler.Handle(@event).ConfigureAwait(false);
            }
        }
    }
}
