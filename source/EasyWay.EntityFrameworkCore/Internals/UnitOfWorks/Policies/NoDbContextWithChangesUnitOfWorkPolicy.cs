using Microsoft.EntityFrameworkCore;

namespace EasyWay.Internals.UnitOfWorks.Policies
{
    internal sealed class NoDbContextWithChangesUnitOfWorkPolicy : IEntityFrameworkUnitOfWorkPolicy
    {
        public Task Apply(IEnumerable<DbContext> dbContextsWithChanges)
        {
            return Task.CompletedTask;
        }

        public bool IsApplicable(IEnumerable<DbContext> dbContextsWithChanges)
            => dbContextsWithChanges.Count() == 0;
    }
}
