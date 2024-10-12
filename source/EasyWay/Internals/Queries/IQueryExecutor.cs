namespace EasyWay.Internals.Queries
{
    internal interface IQueryExecutor<TModule>
        where TModule : EasyWayModule
    {
        Task<TResult> Execute<TQuery, TResult>(TQuery query, CancellationToken cancellationToken)
            where TQuery : Query<TModule, TResult>
            where TResult : ReadModel;
    }
}
