using EasyWay.Internals;
using EasyWay.Internals.BusinessRules;
using EasyWay.Internals.Commands;
using System.Data;

namespace EasyWay
{
    public sealed class CommandResult
    {
        internal CommandErrorEnum Error { get; }

        internal IDictionary<string, string[]> ValidationErrors;

        internal BrokenBusinessRuleException? BrokenBusinessRuleException;

        internal Exception? Exception;

        private CommandResult() 
        {
            Error = CommandErrorEnum.None;
            ValidationErrors = new Dictionary<string, string[]>();
            Exception = null;
        }

        private CommandResult(CommandErrorEnum error)
        {
            Error = error;
            ValidationErrors = new Dictionary<string, string[]>();
            Exception = null;
        }

        private CommandResult(IDictionary<string, string[]> validationErrors)
        {
            Error = CommandErrorEnum.Validation;
            ValidationErrors = validationErrors;
            Exception = null;
        }

        private CommandResult(BrokenBusinessRuleException brokenBusinessRuleException)
        {
            Error = CommandErrorEnum.BrokenBusinessRule;
            ValidationErrors = new Dictionary<string, string[]>();
            BrokenBusinessRuleException = brokenBusinessRuleException;
        }

        private CommandResult(ConcurrencyException concurrencyException)
        {
            Error = CommandErrorEnum.ConcurrencyConflict;
            ValidationErrors = new Dictionary<string, string[]>();
            Exception = concurrencyException;
        }

        public CommandResult(Exception exception)
        {
            Error = CommandErrorEnum.UnknownException;
            ValidationErrors = new Dictionary<string, string[]>();
            Exception = exception;
        }

        internal static CommandResult BrokenBusinessRule(BrokenBusinessRuleException brokenBusinessRuleException) => new CommandResult(brokenBusinessRuleException);

        internal static CommandResult Validation(IDictionary<string, string[]> validationErrors) => new CommandResult(validationErrors);

        internal static CommandResult ConcurrencyConflict(ConcurrencyException concurrencyException) => new CommandResult(concurrencyException);

        internal static CommandResult OperationCanceled() => new CommandResult(CommandErrorEnum.OperationCanceled);

        internal static CommandResult UnknownException(Exception exception) => new CommandResult(exception);

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

        internal Exception? Exception;

        private CommandResult(TOperationResult operationResult)
        {
            OperationResult = operationResult;
            Error = CommandErrorEnum.None;
            ValidationErrors = new Dictionary<string, string[]>();
            Exception = null;
        }

        private CommandResult(IDictionary<string, string[]> validationErrors)
        {
            OperationResult = null;
            Error = CommandErrorEnum.Validation;
            ValidationErrors = validationErrors;
            Exception = null;
        }

        private CommandResult(CommandErrorEnum error)
        {
            OperationResult = null;
            Error = error;
            ValidationErrors = new Dictionary<string, string[]>();
            Exception = null;
        }

        private CommandResult(BrokenBusinessRuleException brokenBusinessRuleException)
        {
            OperationResult = null;
            Error = CommandErrorEnum.BrokenBusinessRule;
            ValidationErrors = new Dictionary<string, string[]>();
            BrokenBusinessRuleException = brokenBusinessRuleException;
        }

        private CommandResult(ConcurrencyException concurrencyException)
        {
            Error = CommandErrorEnum.ConcurrencyConflict;
            ValidationErrors = new Dictionary<string, string[]>();
            Exception = concurrencyException;
        }

        public CommandResult(Exception exception)
        {
            Error = CommandErrorEnum.UnknownException;
            ValidationErrors = new Dictionary<string, string[]>();
            Exception = exception;
        }

        internal static CommandResult<TOperationResult> Validation(IDictionary<string, string[]> validationErrors) => new CommandResult<TOperationResult>(validationErrors);

        internal static CommandResult<TOperationResult> BrokenBusinessRule(BrokenBusinessRuleException brokenBusinessRuleException) => new CommandResult<TOperationResult>(brokenBusinessRuleException);

        internal static CommandResult<TOperationResult> ConcurrencyConflict(ConcurrencyException concurrencyException) => new CommandResult<TOperationResult>(concurrencyException);

        internal static CommandResult<TOperationResult> OperationCanceled() => new CommandResult<TOperationResult>(CommandErrorEnum.OperationCanceled);

        internal static CommandResult<TOperationResult> UnknownException(Exception exception) => new CommandResult<TOperationResult>(exception);

        public static CommandResult<TOperationResult> Ok(TOperationResult operationResult) => new CommandResult<TOperationResult>(operationResult);

        public static CommandResult<TOperationResult> NotFound => new CommandResult<TOperationResult>(CommandErrorEnum.NotFound);

        public static CommandResult<TOperationResult> Forbidden => new CommandResult<TOperationResult>(CommandErrorEnum.Forbidden);
    }
}
