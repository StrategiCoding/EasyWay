namespace EasyWay.Samples.Queries
{
    internal sealed class SampleQueryHandler : QueryHandler<SampleQuery, SampleQueryResult>
    {
        public sealed override Task<QueryResult<SampleQueryResult>> Handle(SampleQuery query)
        {
            return Task.FromResult(QueryResult<SampleQueryResult>.Ok(new SampleQueryResult()));
        }
    }
}
