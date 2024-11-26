namespace EasyWay.Internals.Queries
{
    internal interface IQueryExecutor<TModule>
        where TModule : EasyWayModule
    {
        Task<QueryResult<TReadModel>> Execute<TReadModel>(Query<TModule, TReadModel> query, CancellationToken cancellationToken)
            where TReadModel : ReadModel;
    }
}
