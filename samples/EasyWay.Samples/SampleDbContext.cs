using EasyWay.Samples.Domain;
using Microsoft.EntityFrameworkCore;

namespace EasyWay.Samples
{
    public  class SampleDbContext : DbContext
    {
        public DbSet<SampleAggragete> SampleAggragetes { get; set; }

        public SampleDbContext() : base() { }

        public SampleDbContext(DbContextOptions<SampleDbContext> options)
            : base(options) { }
    }
}
