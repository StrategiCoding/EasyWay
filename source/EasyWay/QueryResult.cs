using EasyWay.Internals.Queries.Results;

namespace EasyWay
{
    public sealed class QueryResult<TReadModel>
        where TReadModel : ReadModel
    {
        internal TReadModel? ReadModel { get;  }

        internal QueryErrorEnum Error { get; }

        internal IDictionary<string, string[]> ValidationErrors;

        internal Exception? Exception { get; }

        private QueryResult(TReadModel readModel)
        {
            ReadModel = readModel;
            ValidationErrors = new Dictionary<string, string[]>();
            Error = QueryErrorEnum.None;
            Exception = null;
        }

        private QueryResult(QueryErrorEnum queryError)
        {
            ReadModel = null;
            ValidationErrors = new Dictionary<string, string[]>();
            Error = queryError;
            Exception = null;
        }

        private QueryResult(IDictionary<string, string[]> validationErrors)
        {
            ReadModel = null;
            ValidationErrors = validationErrors;
            Error = QueryErrorEnum.Validation;
            Exception = null;
        }

        private QueryResult(Exception exception)
        {
            ReadModel = null;
            ValidationErrors = new Dictionary<string, string[]>();
            Error = QueryErrorEnum.UnknownException;
            Exception = exception;
        }

        internal static QueryResult<TReadModel> Validation(IDictionary<string, string[]> validationErrors) => new QueryResult<TReadModel>(validationErrors);

        internal static QueryResult<TReadModel> OperationCanceled() => new QueryResult<TReadModel>(QueryErrorEnum.OperationCanceled);

        internal static QueryResult<TReadModel> UnknownException(Exception exception) => new QueryResult<TReadModel>(exception);

        public static QueryResult<TReadModel> Ok(TReadModel readModel) => new QueryResult<TReadModel>(readModel);

        public static QueryResult<TReadModel> NotFound => new QueryResult<TReadModel>(QueryErrorEnum.NotFound);

        public static QueryResult<TReadModel> Forbidden => new QueryResult<TReadModel>(QueryErrorEnum.Forbidden);
    }
}
