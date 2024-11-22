namespace EasyWay
{
    public interface IGivenBuilder
    {
        void UserId(Guid userId);

        void Replace<TService, TImplementation>()
            where TService : class
            where TImplementation : class, TService;

        void  Replace<TService>(TService implementationInstance)
            where TService : class;
    }
}
