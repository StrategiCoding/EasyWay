namespace EasyWay
{
    public abstract class AggregateRoot : Entity
    {
        internal long ConcurrencyToken { get; set; }

        protected AggregateRoot() : base() { }
    }
}
