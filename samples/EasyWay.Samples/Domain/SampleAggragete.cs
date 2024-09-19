namespace EasyWay.Samples.Domain
{
    public sealed class SampleAggragete : AggregateRoot<SampleAggrageteId>
    {

        public SampleAggragete() { }

        public void SampleMethod()
        {
            CheckRule(new SampleBusinessRule());
        }
    }
}
