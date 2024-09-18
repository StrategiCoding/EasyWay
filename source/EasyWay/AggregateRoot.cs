namespace EasyWay
{
    public abstract class AggregateRoot<TAggregateRootId> : Entity<TAggregateRootId>
        where TAggregateRootId : AggregateRootId
    {
        private long _concurrencyToken;

        protected AggregateRoot() : base() { }

        protected void Up() => _concurrencyToken++;
    }

    public abstract class AggregateRoot<TAggregateRootId, TDomainEvent> : Entity<TAggregateRootId, TDomainEvent>
        where TAggregateRootId : AggregateRootId
        where TDomainEvent : DomainEvent
    {
        private long _concurrencyToken;

        protected AggregateRoot() : base() { }

        protected void Up() => _concurrencyToken++;

        protected new void Add(TDomainEvent domainEvent)
        {
            Up();

            base.Add(domainEvent);
        }
    }
}
