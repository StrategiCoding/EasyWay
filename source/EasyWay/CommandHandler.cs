namespace EasyWay
{
    /// <summary>
    /// Defines a handler for a command
    /// </summary>
    /// <typeparam name="TCommand">The type of command being handled</typeparam>
    public abstract class CommandHandler<TCommand>
        where TCommand : Command
    {
        /// <summary>
        /// Handles a command
        /// </summary>
        /// <param name="command">Command</param>
        public abstract Task<CommandResult> Handle(TCommand command);
    }

    public abstract class CommandHandler<TCommand, TOperationResult>
        where TCommand : Command<TOperationResult>
        where TOperationResult : OperationResult
    {
        public abstract Task<CommandResult<TOperationResult>> Handle(TCommand command);
    }
}
