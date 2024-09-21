using Microsoft.Extensions.DependencyInjection;
using System.Threading;

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
            var handlerType = typeof(IDomainEventHandler<>).MakeGenericType(@event.GetType());

            var eventHandlers = _serviceProvider.GetServices(handlerType);

            foreach (var eventHandler in eventHandlers)
            {
                await (Task)handlerType
                    .GetMethod(nameof(IDomainEventHandler<TEvent>.Handle))?
                    .Invoke(eventHandler, new object[] { @event });
            }
        }
    }
}
