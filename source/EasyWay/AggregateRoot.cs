namespace EasyWay
{
    public abstract class AggregateRoot : Entity
    {
        private long _concurrencyToken;

        protected AggregateRoot() : base() { }

        protected void Up() => _concurrencyToken++;

        protected new void Add<TDomainEvent>(TDomainEvent domainEvent)
            where TDomainEvent : DomainEvent
        {
            Up();

            base.Add(domainEvent);
        }
    }
}
