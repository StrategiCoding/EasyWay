using Microsoft.EntityFrameworkCore;

namespace EasyWay.Samples
{
    public  class SampleDbContext : DbContext
    {
        public SampleDbContext() : base() { }

        public SampleDbContext(DbContextOptions<SampleDbContext> options)
            : base(options) { }
    }
}
