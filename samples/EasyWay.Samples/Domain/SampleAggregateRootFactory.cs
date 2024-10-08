namespace EasyWay.Samples.Domain
{
    public sealed class SampleAggregateRootFactory
    {
        public SampleAggregateRoot Create()
        {
            return new SampleAggregateRoot();
        }
    }
}
