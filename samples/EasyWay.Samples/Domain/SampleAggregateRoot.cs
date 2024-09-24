namespace EasyWay.Samples.Domain
{
    public sealed class SampleAggregateRoot : AggregateRoot
    {
        public SampleAggregateRoot() 
        { 
            Add(new CreatedSampleAggragete());
        }

        public void SampleMethod()
        {
            CheckRule(new SampleBusinessRule());
        }
    }
}
