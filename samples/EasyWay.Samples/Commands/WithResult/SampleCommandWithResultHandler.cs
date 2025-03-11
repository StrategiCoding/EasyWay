
namespace EasyWay.Samples.Commands.WithResult
{
    public sealed class SampleCommandWithResultHandler : CommandHandler<SampleCommandWithResult, SampleCommandResult>
    {
        public sealed override Task<CommandResult<SampleCommandResult>> Handle(SampleCommandWithResult command)
        {
            return Task.FromResult(CommandResult<SampleCommandResult>.Ok(new SampleCommandResult()));
        }
    }
}
