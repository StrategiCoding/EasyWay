using EasyWay.Samples.Domain;
using Microsoft.EntityFrameworkCore;

namespace EasyWay.Samples.Databases
{
    public class SampleDbContext : DbContext
    {
        public DbSet<SampleAggregateRoot> SampleAggragetes { get; set; }

        public SampleDbContext() : base() { }

        public SampleDbContext(DbContextOptions<SampleDbContext> options)
            : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SampleDbContext).Assembly);
        }
    }
}
