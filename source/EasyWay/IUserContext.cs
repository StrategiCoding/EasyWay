namespace EasyWay
{
    public interface IUserContext
    {
        Guid? UserId { get; }

        bool IsAuthenticated { get; }
    }
}
