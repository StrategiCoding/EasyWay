namespace EasyWay
{
    public interface IJwtFactory
    {
        Tokens Create(Guid userId, string role);
    }
}
