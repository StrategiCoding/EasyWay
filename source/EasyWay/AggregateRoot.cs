namespace EasyWay
{
    public abstract class AggregateRoot : Entity

    {
        private long _concurrencyToken;

        protected AggregateRoot() : base() { }

        protected void Up() => _concurrencyToken++;
    }

    public abstract class AggregateRoot<TDomainEvent> : Entity<TDomainEvent>
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
