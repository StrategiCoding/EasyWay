namespace EasyWay.Internals.AccessTokenCreators
{
    internal interface IAccessTokensCreator
    {
        string Create(Guid userId);
    }
}
