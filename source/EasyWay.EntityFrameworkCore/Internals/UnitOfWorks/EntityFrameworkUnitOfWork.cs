using Microsoft.EntityFrameworkCore;

namespace EasyWay.Internals.UnitOfWorks
{
    internal sealed class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private readonly IEnumerable<DbContext> _contexts;

        public EntityFrameworkUnitOfWork(IEnumerable<DbContext> contexts)
        {
            _contexts = contexts;
        }

        public async Task Commit()
        {
            try
            {
                foreach (var context in _contexts) 
                {
                    await context.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ConcurrencyException(ex.Message, ex);
            }
        }
    }
}
