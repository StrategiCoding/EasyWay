namespace EasyWay.Samples.Domain
{
    public sealed class SampleAggregateRootFactory : Factory
    {
        public SampleAggregateRoot Create()
        {
            return new SampleAggregateRoot();
        }
    }
}
