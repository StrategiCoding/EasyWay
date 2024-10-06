using EasyWay.Internals.UnitOfWorks.Policies;
using Microsoft.EntityFrameworkCore;

namespace EasyWay.Internals.UnitOfWorks
{
    internal sealed class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private readonly IEnumerable<DbContext> _contexts;

        private readonly IEnumerable<IEntityFrameworkUnitOfWorkPolicy> _policies;

        public EntityFrameworkUnitOfWork(
            IEnumerable<DbContext> contexts,
            IEnumerable<IEntityFrameworkUnitOfWorkPolicy> policies)
        {
            _contexts = contexts;
            _policies = policies;
        }

        public async Task Commit()
        {
            var contextsWithChanges = _contexts.Where(x => x.ChangeTracker.HasChanges());

            try
            {
                await _policies
                    .Where(x => x.IsApplicable(contextsWithChanges))
                    .Single()
                    .Apply(contextsWithChanges)
                    .ConfigureAwait(false);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ConcurrencyException(ex.Message, ex);
            }
        }
    }
}
