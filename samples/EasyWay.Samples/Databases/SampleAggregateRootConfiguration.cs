using EasyWay.EntityFrameworkCore;
using EasyWay.Samples.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyWay.Samples.Databases
{
    public class SampleAggregateRootConfiguration : AggregateRootConfiguration<SampleAggregateRoot>
    {
        public override void ConfigureAggregateRoot(EntityTypeBuilder<SampleAggregateRoot> builder)
        {
            
        }
    }
}
