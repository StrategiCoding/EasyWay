namespace EasyWay.Internals.UnitOfWorks
{
    internal interface IUnitOfWork
    {
        Task Commit();
    }
}
