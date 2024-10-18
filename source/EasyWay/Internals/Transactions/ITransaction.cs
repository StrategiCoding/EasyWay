namespace EasyWay.Internals.Transactions
{
    internal interface ITransaction
    {
        Task Commit();
    }
}
