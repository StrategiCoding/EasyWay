namespace EasyWay.Internals.Contexts
{
    internal class DefaultUserContext : IUserContext
    {
        public string? UserId => null;

        public bool IsAuthenticated => false;
    }
}
