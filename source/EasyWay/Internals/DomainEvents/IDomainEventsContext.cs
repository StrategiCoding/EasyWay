namespace EasyWay.Internals.DomainEvents
{
    internal interface IDomainEventsContext
    {
        IReadOnlyCollection<DomainEvent> GetAllDomainEvents();

        void ClearAllDomainEvents();
    }
}
