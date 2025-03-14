namespace EasyWay.Internals.Commands
{
    internal interface ITransaction
    {
        Task Commit();
    }
}
