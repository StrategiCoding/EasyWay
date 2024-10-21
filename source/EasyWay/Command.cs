namespace EasyWay
{
    /// <summary>
    /// Represents a command
    /// </summary>
    public abstract class Command<TModule>
        where TModule : EasyWayModule;

    public abstract class Command<TModule, TCommandResult>
        where TModule : EasyWayModule
        where TCommandResult : CommandResult;
}
