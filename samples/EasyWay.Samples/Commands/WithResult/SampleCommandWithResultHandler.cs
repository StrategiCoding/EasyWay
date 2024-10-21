
namespace EasyWay.Samples.Commands.WithResult
{
    public sealed class SampleCommandWithResultHandler : ICommandHandler<SampleModule, SampleCommandWithResult, SampleCommandResult>
    {
        public Task<SampleCommandResult> Handle(SampleCommandWithResult command)
        {
            return Task.FromResult(new SampleCommandResult());
        }
    }
}
