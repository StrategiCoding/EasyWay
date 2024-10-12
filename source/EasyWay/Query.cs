namespace EasyWay
{
    /// <summary>
    /// Represents a query
    /// </summary>
    /// <typeparam name="TReadModel">Read model type</typeparam>
    public abstract class Query<TModule,TReadModel>
        where TModule : EasyWayModule
        where TReadModel : ReadModel;
}
