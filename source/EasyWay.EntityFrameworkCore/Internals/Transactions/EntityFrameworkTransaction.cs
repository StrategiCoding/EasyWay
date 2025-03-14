using EasyWay.Internals.Commands;
using EasyWay.Internals.Transactions.Policies;
using Microsoft.EntityFrameworkCore;

namespace EasyWay.Internals.Transactions
{
    internal sealed class EntityFrameworkTransaction : ITransaction
    {
        private readonly IEnumerable<DbContext> _contexts;

        private readonly IEnumerable<IEntityFrameworkTransactionPolicy> _policies;

        public EntityFrameworkTransaction(
            IEnumerable<DbContext> contexts,
            IEnumerable<IEntityFrameworkTransactionPolicy> policies)
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
