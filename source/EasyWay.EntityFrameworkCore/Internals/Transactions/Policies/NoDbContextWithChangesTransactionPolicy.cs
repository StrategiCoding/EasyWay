using Microsoft.EntityFrameworkCore;

namespace EasyWay.Internals.Transactions.Policies
{
    internal sealed class NoDbContextWithChangesTransactionPolicy : IEntityFrameworkTransactionPolicy
    {
        public Task Apply(IEnumerable<DbContext> dbContextsWithChanges)
        {
            return Task.CompletedTask;
        }

        public bool IsApplicable(IEnumerable<DbContext> dbContextsWithChanges)
            => dbContextsWithChanges.Count() == 0;
    }
}
