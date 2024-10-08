namespace EasyWay.Samples.Domain
{
    public sealed class SampleAggregateRoot : AggregateRoot
    {
        internal SampleAggregateRoot() 
        { 
            Add(new CreatedSampleAggragete());
        }

        public void SampleMethod()
        {
            Check(new SampleBusinessRule());
        }
    }
}
