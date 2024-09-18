
namespace EasyWay.Samples
{
    internal sealed class SampleCommandHandler : ICommandHandler<SampleCommand>
    {
        private readonly ICancellationTokenProvider _tokenProvider;

        public SampleCommandHandler(ICancellationTokenProvider tokenProvider)
        {
            _tokenProvider = tokenProvider;
        }

        public Task Handle(SampleCommand command)
        {
            var token = _tokenProvider.Token;

            return Task.CompletedTask;
        }
    }
}
