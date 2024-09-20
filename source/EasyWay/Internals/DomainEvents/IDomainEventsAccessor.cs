namespace EasyWay.Internals.DomainEvents
{
    internal interface IDomainEventsAccessor
    {
        IReadOnlyCollection<DomainEvent> GetAllDomainEvents();

        void ClearAllDomainEvents();
    }
}
