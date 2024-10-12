namespace EasyWay
{
    public interface IWithConcurrencyToken
    {
        short ConcurrencyToken { get; }
    }
}
