namespace EasyWay
{
    /// <summary>
    /// Defines a handler for a query
    /// </summary>
    /// <typeparam name="TQuery">The type of query being handled</typeparam>
    public interface IQueryHandler<TQuery, TReadModel>
        where TQuery : Query<TReadModel>
        where TReadModel : ReadModel
    {
        /// <summary>
        /// Handles a query
        /// </summary>
        /// <param name="query">Query</param>
        Task<TReadModel> Handle(TQuery query);
    }
}
