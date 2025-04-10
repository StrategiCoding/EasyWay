namespace EasyWay.Samples.Domain
{
    public sealed class SampleAggregateRoot : AggregateRoot
    {
        internal SampleAggregateRoot() 
        { 
            Apply(new CreatedSampleAggragete());
        }

        private void When(CreatedSampleAggragete @event)
        {

        }

        public void SampleMethod()
        {
            Check(new SampleBusinessRule());
        }
    }
}
