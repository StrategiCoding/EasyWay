using Microsoft.EntityFrameworkCore;

namespace EasyWay.Internals.Transactions.Policies
{
    internal sealed class OneDbContextWithChangesTransactionPolicy : IEntityFrameworkTransactionPolicy
    {
        public async Task Apply(IEnumerable<DbContext> dbContextsWithChanges)
        {
            await dbContextsWithChanges
                .Single()
                .SaveChangesAsync()
                .ConfigureAwait(false);
        }

        public bool IsApplicable(IEnumerable<DbContext> dbContextsWithChanges)
            => dbContextsWithChanges.Count() == 1;
    }
}
