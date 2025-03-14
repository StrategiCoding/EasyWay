namespace EasyWay.Internals.DomainEvents
{
    internal interface IDomainEventsContext
    {
        IReadOnlyCollection<DomainEventContext> GetAllDomainEvents();

        void ClearAllDomainEvents();
    }
}
