namespace EasyWay.Samples.Domain
{
    public sealed class SampleAggragete : AggregateRoot
    {
        public SampleAggragete() { }

        public void SampleMethod()
        {
            CheckRule(new SampleBusinessRule());
        }
    }
}
