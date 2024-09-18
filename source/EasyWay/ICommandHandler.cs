namespace EasyWay
{
    /// <summary>
    /// Defines a handler for a command
    /// </summary>
    /// <typeparam name="TCommand">The type of command being handled</typeparam>
    public interface ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        /// <summary>
        /// Handles a command
        /// </summary>
        /// <param name="command">Command</param>
        Task Handle(TCommand command);
    }
}
