namespace EasyWay.Samples.Queries
{
    public class SampleQueryHandler : IQueryHandler<SampleModule, SampleQuery, SampleQueryResult>
    {
        public Task<SampleQueryResult> Handle(SampleQuery query)
        {
            return Task.FromResult(new SampleQueryResult());
        }
    }
}
