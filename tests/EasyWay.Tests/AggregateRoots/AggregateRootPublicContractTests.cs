using EasyWay.Tests.SeedWorks;

namespace EasyWay.Tests.AggregateRoots
{
    public sealed class AggregateRootPublicContractTests
    {
        private readonly Type _aggregateRootType = typeof(AggregateRoot);

        [Fact(DisplayName = $"{nameof(AggregateRoot)} public contract test")]
        public void PublicContractTest()
        {
            _aggregateRootType
                .IsPublic()
                .IsAbstract()
                .InheritanceFrom(typeof(Entity))
                .HasFullName("EasyWay.AggregateRoot");
        }
    }
}
