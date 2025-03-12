namespace EasyWay.Internals.Queries
{
    internal interface IQueryExecutor
    {
        Task<QueryResult<TReadModel>> Execute<TModule, TQuery, TReadModel>(TQuery query, CancellationToken cancellationToken = default)
            where TModule : EasyWayModule
            where TQuery : Query<TReadModel>
            where TReadModel : ReadModel;
    }
}
