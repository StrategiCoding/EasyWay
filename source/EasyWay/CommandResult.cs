using EasyWay.Internals.Commands.Results;

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
}
