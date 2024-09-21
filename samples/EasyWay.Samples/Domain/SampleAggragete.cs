namespace EasyWay.Samples.Domain
{
    public sealed class SampleAggragete : AggregateRoot
    {
        public SampleAggragete() 
        { 
            Add(new CreatedSampleAggragete());
        }

        public void SampleMethod()
        {
            CheckRule(new SampleBusinessRule());
        }
    }
}
