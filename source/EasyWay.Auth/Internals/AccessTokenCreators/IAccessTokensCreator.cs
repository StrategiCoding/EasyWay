namespace EasyWay.Internals.AccessTokenCreators
{
    internal interface IAccessTokensCreator
    {
        AccessToken Create(Guid userId);
    }
}
