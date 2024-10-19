namespace EasyWay
{
    public interface IJwtFactory
    {
        string Create(Guid userId, string role);
    }
}
