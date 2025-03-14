using EasyWay.Internals.BusinessRules;
using EasyWay.Internals.Commands.CommandsWithResult;

namespace EasyWay
{
    public sealed class CommandResult
    {
        internal CommandErrorEnum Error { get; }

        internal IDictionary<string, string[]> ValidationErrors;

        internal BrokenBusinessRuleException? BrokenBusinessRuleException;

        private CommandResult() 
        {
            Error = CommandErrorEnum.None;
            ValidationErrors = new Dictionary<string, string[]>();
            BrokenBusinessRuleException = null;
        }

        private CommandResult(CommandErrorEnum error)
        {
            Error = error;
            ValidationErrors = new Dictionary<string, string[]>();
            BrokenBusinessRuleException = null;
        }

        private CommandResult(IDictionary<string, string[]> validationErrors)
        {
            Error = CommandErrorEnum.Validation;
            ValidationErrors = validationErrors;
            BrokenBusinessRuleException = null;
        }

        private CommandResult(BrokenBusinessRuleException brokenBusinessRuleException)
        {
            Error = CommandErrorEnum.BrokenBusinessRule;
            ValidationErrors = new Dictionary<string, string[]>();
            BrokenBusinessRuleException = brokenBusinessRuleException;
        }

        internal static CommandResult BrokenBusinessRule(BrokenBusinessRuleException brokenBusinessRuleException) => new CommandResult(brokenBusinessRuleException);

        internal static CommandResult Validation(IDictionary<string, string[]> validationErrors) => new CommandResult(validationErrors);

        public static CommandResult Ok => new CommandResult();

        public static CommandResult NotFound => new CommandResult(CommandErrorEnum.NotFound);

        public static CommandResult Forbidden => new CommandResult(CommandErrorEnum.Forbidden);
    }

    public sealed class CommandResult<TOperationResult>
        where TOperationResult : OperationResult
    {
        internal TOperationResult? OperationResult { get; }

        internal CommandErrorEnum Error { get; }

        internal IDictionary<string, string[]> ValidationErrors;

        internal BrokenBusinessRuleException? BrokenBusinessRuleException;

        private CommandResult(TOperationResult operationResult)
        {
            OperationResult = operationResult;
            Error = CommandErrorEnum.None;
            ValidationErrors = new Dictionary<string, string[]>();
        }

        private CommandResult(IDictionary<string, string[]> validationErrors)
        {
            OperationResult = null;
            Error = CommandErrorEnum.Validation;
            ValidationErrors = validationErrors;
        }

        private CommandResult(CommandErrorEnum error)
        {
            OperationResult = null;
            Error = error;
            ValidationErrors = new Dictionary<string, string[]>();
        }

        private CommandResult(BrokenBusinessRuleException brokenBusinessRuleException)
        {
            OperationResult = null;
            Error = CommandErrorEnum.BrokenBusinessRule;
            ValidationErrors = new Dictionary<string, string[]>();
            BrokenBusinessRuleException = brokenBusinessRuleException;
        }

        internal static CommandResult<TOperationResult> Validation(IDictionary<string, string[]> validationErrors) => new CommandResult<TOperationResult>(validationErrors);

        internal static CommandResult<TOperationResult> BrokenBusinessRule(BrokenBusinessRuleException brokenBusinessRuleException) => new CommandResult<TOperationResult>(brokenBusinessRuleException);

        public static CommandResult<TOperationResult> Ok(TOperationResult operationResult) => new CommandResult<TOperationResult>(operationResult);

        public static CommandResult<TOperationResult> NotFound => new CommandResult<TOperationResult>(CommandErrorEnum.NotFound);

        public static CommandResult<TOperationResult> Forbidden => new CommandResult<TOperationResult>(CommandErrorEnum.Forbidden);
    }
}
