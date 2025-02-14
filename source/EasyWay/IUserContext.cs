namespace EasyWay
{
    public interface IUserContext
    {
        string? UserId { get; }

        bool IsAuthenticated { get; }
    }
}
