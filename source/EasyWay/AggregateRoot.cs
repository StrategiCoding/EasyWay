namespace EasyWay
{
    public abstract class AggregateRoot : Entity
    {
        internal short ConcurrencyToken { get; private set; }

        internal void UpdateConcurrencyToken()
        {
            if (ConcurrencyToken == short.MaxValue)
            {
                ConcurrencyToken = short.MinValue;
            }
            else
            {
                ConcurrencyToken++;
            }
        }

        protected AggregateRoot() : base() { }
    }
}
