namespace EasyWay
{
    public abstract class BusinessRule
    {
        protected internal abstract string Message { get; }

        protected internal abstract bool IsFulfilled();
    }
}
