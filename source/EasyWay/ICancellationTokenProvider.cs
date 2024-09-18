namespace EasyWay
{
    public interface ICancellationTokenProvider
    {
        CancellationToken Token { get; }
    }
}
