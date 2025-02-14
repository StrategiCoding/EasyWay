namespace EasyWay.Internals.Contexts
{
    internal sealed class DefaultUserContext : IUserContext
    {
        public string? UserId => null;

        public bool IsAuthenticated => false;
    }
}
