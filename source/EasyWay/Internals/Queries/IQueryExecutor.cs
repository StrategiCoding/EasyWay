namespace EasyWay.Internals.Queries
{
    internal interface IQueryExecutor
    {
        Task<TResult> Execute<TQuery, TResult>(TQuery query, CancellationToken cancellationToken)
            where TQuery : Query<TResult>
            where TResult : ReadModel;
    }
}
