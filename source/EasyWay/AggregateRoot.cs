namespace EasyWay
{
    public abstract class AggregateRoot : Entity
    {
        internal long ConcurrencyToken { get; private set; }

        protected AggregateRoot() : base() { }

        protected void Up() => ConcurrencyToken++;

        protected new void Add<TDomainEvent>(TDomainEvent domainEvent)
            where TDomainEvent : DomainEvent
        {
            Up();

            base.Add(domainEvent);
        }
    }
}
