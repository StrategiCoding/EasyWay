namespace EasyWay
{
    /// <summary>
    /// Represents a command
    /// </summary>
    public abstract class Command;

    public abstract class Command<TCommandResult>
        where TCommandResult : OperationResult;
}
