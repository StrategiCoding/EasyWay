namespace EasyWay
{
    /// <summary>
    /// Defines a handler for a command
    /// </summary>
    /// <typeparam name="TCommand">The type of command being handled</typeparam>
    public interface ICommandHandler<TCommand>
        where TCommand : Command
    {
        /// <summary>
        /// Handles a command
        /// </summary>
        /// <param name="command">Command</param>
        Task<CommandResult> Handle(TCommand command);
    }

    public interface ICommandHandler<TCommand, TOperationResult>
        where TCommand : Command<TOperationResult>
        where TOperationResult : OperationResult
    {
        Task<CommandResult<TOperationResult>> Handle(TCommand command);
    }
}
