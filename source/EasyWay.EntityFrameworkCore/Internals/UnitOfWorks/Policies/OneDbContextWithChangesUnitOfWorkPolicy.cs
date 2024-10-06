using Microsoft.EntityFrameworkCore;

namespace EasyWay.Internals.UnitOfWorks.Policies
{
    internal sealed class OneDbContextWithChangesUnitOfWorkPolicy : IEntityFrameworkUnitOfWorkPolicy
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
