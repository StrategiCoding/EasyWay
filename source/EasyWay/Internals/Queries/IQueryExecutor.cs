namespace EasyWay.Internals.Queries
{
    internal interface IQueryExecutor<TModule>
        where TModule : EasyWayModule
    {
        Task<QueryResult<TReadModel>> Execute<TQuery, TReadModel>(TQuery query, CancellationToken cancellationToken)
            where TQuery : Query<TModule, TReadModel>
            where TReadModel : ReadModel;
    }
}
