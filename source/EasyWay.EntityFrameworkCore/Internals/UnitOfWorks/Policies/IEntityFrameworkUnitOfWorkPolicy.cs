using Microsoft.EntityFrameworkCore;

namespace EasyWay.Internals.UnitOfWorks.Policies
{
    internal interface IEntityFrameworkUnitOfWorkPolicy
    {
        bool IsApplicable(IEnumerable<DbContext> dbContextsWithChanges);

        Task Apply(IEnumerable<DbContext> dbContextsWithChanges);

    }
}
