
namespace EasyWay.Samples.Commands.WithResult
{
    public sealed class SampleCommandWithResultHandler : ICommandHandler<SampleModule, SampleCommandWithResult, SampleCommandResult>
    {
        public Task<CommandResult<SampleCommandResult>> Handle(SampleCommandWithResult command)
        {
            return Task.FromResult(CommandResult<SampleCommandResult>.Ok(new SampleCommandResult()));
        }
    }
}
