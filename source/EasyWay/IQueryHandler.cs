namespace EasyWay
{
    /// <summary>
    /// Defines a handler for a query
    /// </summary>
    /// <typeparam name="TQuery">The type of query being handled</typeparam>
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        /// <summary>
        /// Handles a query
        /// </summary>
        /// <param name="query">Query</param>
        Task<TResult> Handle(TQuery query);
    }
}
