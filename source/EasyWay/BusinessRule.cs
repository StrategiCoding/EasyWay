namespace EasyWay
{
    public abstract class BusinessRule
    {
        protected internal abstract string Message { get; }

        protected internal abstract bool IsFulfilled();

        public sealed override string ToString() => $"{GetType().FullName}: {Message}";
    }
}
