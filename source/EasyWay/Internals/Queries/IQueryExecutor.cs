namespace EasyWay.Internals.Queries
{
    internal interface IQueryExecutor<TModule>
        where TModule : EasyWayModule
    {
        Task<QueryResult<TReadModel>> Execute<TQuery, TReadModel>(TQuery query, CancellationToken cancellationToken = default)
            where TQuery : Query<TReadModel>
            where TReadModel : ReadModel;
    }
}
