namespace EasyWay.Internals.Commands
{
    internal interface IUnitOfWorkCommandHandler
    {
        Task Handle();
    }
}
