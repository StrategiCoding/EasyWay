
namespace EasyWay.Samples
{
    internal sealed class SampleCommandHandler : ICommandHandler<SampleCommand>
    {
        public Task Handle(SampleCommand command, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
