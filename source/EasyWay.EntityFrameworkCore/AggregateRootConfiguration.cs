using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace EasyWay
{
    public abstract class AggregateRootConfiguration<TAggregateRoot> : IEntityTypeConfiguration<TAggregateRoot>
        where TAggregateRoot : AggregateRoot
    {
        public virtual void Configure(EntityTypeBuilder<TAggregateRoot> builder)
        {
            builder.Ignore(x => x.DomainEvents);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.ConcurrencyToken).IsConcurrencyToken();

            ConfigureAggregateRoot(builder);
        }

        public abstract void ConfigureAggregateRoot(EntityTypeBuilder<TAggregateRoot> builder);
    }
}
