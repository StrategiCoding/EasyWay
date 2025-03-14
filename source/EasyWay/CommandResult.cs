using EasyWay.Internals.Commands.CommandsWithResult;

namespace EasyWay
{
    public sealed class CommandResult
    {
        internal CommandErrorEnum Error { get; }

        internal IDictionary<string, string[]> ValidationErrors;

        private CommandResult() 
        {
            Error = CommandErrorEnum.None;
            ValidationErrors = new Dictionary<string, string[]>();
        }

        private CommandResult(IDictionary<string, string[]> validationErrors)
        {
            Error = CommandErrorEnum.Validation;
            ValidationErrors = validationErrors;
        }

        internal static CommandResult Validation(IDictionary<string, string[]> validationErrors) => new CommandResult(validationErrors);

        public static CommandResult Ok => new CommandResult();
    }

    public sealed class CommandResult<TOperationResult>
        where TOperationResult : OperationResult
    {
        internal TOperationResult OperationResult { get; }

        internal CommandErrorEnum Error { get; }

        internal IDictionary<string, string[]> ValidationErrors;

        private CommandResult(TOperationResult operationResult)
        {
            OperationResult = operationResult;
            Error = CommandErrorEnum.None;
            ValidationErrors = new Dictionary<string, string[]>();
        }

        private CommandResult(IDictionary<string, string[]> validationErrors)
        {
            Error = CommandErrorEnum.Validation;
            ValidationErrors = validationErrors;
        }

        internal static CommandResult<TOperationResult> Validation(IDictionary<string, string[]> validationErrors) => new CommandResult<TOperationResult>(validationErrors);

        public static CommandResult<TOperationResult> Ok(TOperationResult operationResult) => new CommandResult<TOperationResult>(operationResult);
    }
}
