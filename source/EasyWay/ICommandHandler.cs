namespace EasyWay
{
    /// <summary>
    /// Defines a handler for a command
    /// </summary>
    /// <typeparam name="TCommand">The type of command being handled</typeparam>
    public interface ICommandHandler<TModule, TCommand>
        where TModule : EasyWayModule
        where TCommand : Command<TModule>
    {
        /// <summary>
        /// Handles a command
        /// </summary>
        /// <param name="command">Command</param>
        Task<CommandResult> Handle(TCommand command);
    }

    public interface ICommandHandler<TModule, TCommand, TOperationResult>
        where TModule : EasyWayModule
        where TCommand : Command<TModule, TOperationResult>
        where TOperationResult : OperationResult
    {
        Task<CommandResult<TOperationResult>> Handle(TCommand command);
    }
}
