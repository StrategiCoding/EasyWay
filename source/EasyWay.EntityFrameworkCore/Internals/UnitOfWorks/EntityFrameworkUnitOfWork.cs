using EasyWay.Internals.UnitOfWorks;
using Microsoft.EntityFrameworkCore;

namespace EasyWay.EntityFrameworkCore.Internals.UnitOfWorks
{
    internal sealed class EntityFrameworkUnitOfWork : IUnitOfWork
    {
        private readonly DbContext _context;

        public EntityFrameworkUnitOfWork(DbContext context)
        {
            _context = context;
        }

        public async Task Commit()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ConcurrencyException(ex.Message, ex);
            }
        }
    }
}
