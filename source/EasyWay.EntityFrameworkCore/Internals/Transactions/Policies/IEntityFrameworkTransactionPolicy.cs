using Microsoft.EntityFrameworkCore;

namespace EasyWay.Internals.Transactions.Policies
{
    internal interface IEntityFrameworkTransactionPolicy
    {
        bool IsApplicable(IEnumerable<DbContext> dbContextsWithChanges);

        Task Apply(IEnumerable<DbContext> dbContextsWithChanges);

    }
}
