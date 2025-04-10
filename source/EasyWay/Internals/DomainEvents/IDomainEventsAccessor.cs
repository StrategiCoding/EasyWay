namespace EasyWay.Internals.DomainEvents
{
    internal interface IDomainEventsAccessor
    {
        IReadOnlyCollection<DomainEventContext> GetAllDomainEvents();

        void ClearAllDomainEvents();
    }
}
