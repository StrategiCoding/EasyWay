namespace EasyWay.Samples.Queries
{
    public class SampleQueryHandler : IQueryHandler<SampleModule, SampleQuery, SampleQueryResult>
    {
        public Task<QueryResult<SampleQueryResult>> Handle(SampleQuery query)
        {
            //return Task.FromResult(QueryResult<SampleQueryResult>.Forbidden);

            return Task.FromResult(QueryResult<SampleQueryResult>.Ok(new SampleQueryResult()));
        }
    }
}
