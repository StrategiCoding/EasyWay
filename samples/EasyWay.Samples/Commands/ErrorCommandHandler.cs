using EasyWay.Samples.Domain;

namespace EasyWay.Samples.Commands
{
    public sealed class ErrorCommandHandler : CommandHandler<ErrorCommand>
    {
        public sealed override Task<CommandResult> Handle(ErrorCommand command)
        {
            var x = new SampleAggregateRoot();

            x.SampleMethod();

            return Task.FromResult(CommandResult.Ok);
        }
    }
}
