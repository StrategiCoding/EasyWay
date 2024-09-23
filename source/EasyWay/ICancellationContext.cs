namespace EasyWay
{
    public interface ICancellationContext
    {
        CancellationToken Token { get; }
    }
}
