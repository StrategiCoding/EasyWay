namespace EasyWay
{
    public interface ITokensCreator
    {
        Tokens Create(Guid userId, string role);
    }
}
