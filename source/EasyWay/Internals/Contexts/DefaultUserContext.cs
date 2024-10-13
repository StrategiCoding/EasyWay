namespace EasyWay.Internals.Contexts
{
    internal class DefaultUserContext : IUserContext
    {
        public Guid? UserId => null;

        public bool IsAuthenticated => false;
    }
}
