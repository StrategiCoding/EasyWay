using EasyWay.Internals.Queries.Results;
using FluentValidation.Results;

namespace EasyWay
{
    public sealed class QueryResult<TReadModel>
        where TReadModel : ReadModel
    {
        internal TReadModel ReadModel { get;  }

        internal QueryErrorEnum Error { get; }

        internal IDictionary<string, string[]> Errors;

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

        private QueryResult(IDictionary<string, string[]> errors)
        {
            Errors = errors;
            Error = QueryErrorEnum.Validation;
        }

        public static QueryResult<TReadModel> Ok(TReadModel readModel) => new QueryResult<TReadModel>(readModel);

        public static QueryResult<TReadModel> Validation(IDictionary<string, string[]> errors) => new QueryResult<TReadModel>(errors);

        public static QueryResult<TReadModel> NotFound => new QueryResult<TReadModel>(QueryErrorEnum.NotFound);

        public static QueryResult<TReadModel> Forbidden => new QueryResult<TReadModel>(QueryErrorEnum.Forbidden);
    }
}
