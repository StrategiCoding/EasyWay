
namespace EasyWay.Samples
{
    public class SampleQueryHandler : IQueryHandler<SampleQuery, SampleQueryResult>
    {
        public Task<SampleQueryResult> Handle(SampleQuery query)
        {
            return Task.FromResult(new SampleQueryResult());
        }
    }
}
