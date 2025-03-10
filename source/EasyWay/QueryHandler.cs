namespace EasyWay
{
    /// <summary>
    /// Defines a handler for a query
    /// </summary>
    /// <typeparam name="TQuery">The type of query being handled</typeparam>
    public abstract class QueryHandler<TQuery, TReadModel>
        where TQuery : Query<TReadModel>
        where TReadModel : ReadModel
    {
        /// <summary>
        /// Handles a query
        /// </summary>
        /// <param name="query">Query</param>
        public abstract Task<QueryResult<TReadModel>> Handle(TQuery query);
    }
}
