using EasyWay.Internals.Queries.Results;

namespace EasyWay
{
    public sealed class QueryResult<TReadModel>
        where TReadModel : ReadModel
    {
        internal TReadModel ReadModel { get;  }

        internal QueryErrorEnum Error { get; }

        private QueryResult(TReadModel readModel)
        {
            ReadModel = readModel;
            Error = QueryErrorEnum.None;
        }

        private QueryResult(QueryErrorEnum queryError)
        {
            ReadModel = null;
            Error = queryError;
        }

        public static QueryResult<TReadModel> Ok(TReadModel readModel) => new QueryResult<TReadModel>(readModel);

        public static QueryResult<TReadModel> NotFound => new QueryResult<TReadModel>(QueryErrorEnum.NotFound);

        public static QueryResult<TReadModel> Forbidden => new QueryResult<TReadModel>(QueryErrorEnum.Forbidden);
    }
}
