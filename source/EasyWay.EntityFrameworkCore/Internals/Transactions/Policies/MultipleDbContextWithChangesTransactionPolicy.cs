using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace EasyWay.Internals.Transactions.Policies
{
    internal sealed class MultipleDbContextWithChangesTransactionPolicy : IEntityFrameworkTransactionPolicy
    {
        public async Task Apply(IEnumerable<DbContext> dbContextsWithChanges)
        {
            var contexts = dbContextsWithChanges.ToList();

            var firstContext = contexts[0];

            contexts.Remove(firstContext);

            using var transaction = await firstContext.Database.BeginTransactionAsync().ConfigureAwait(false);

            try
            {
                await firstContext.SaveChangesAsync().ConfigureAwait(false);

                foreach (var context in contexts)
                {
                    await context.Database.UseTransactionAsync(transaction.GetDbTransaction()).ConfigureAwait(false); ;
                    await context.SaveChangesAsync().ConfigureAwait(false);
                }

                await transaction.CommitAsync().ConfigureAwait(false);
            }
            catch (Exception)
            {
                await transaction.RollbackAsync().ConfigureAwait(false);
                throw;
            }
        }

        public bool IsApplicable(IEnumerable<DbContext> dbContextsWithChanges)
            => dbContextsWithChanges.Count() > 1;
    }
}
