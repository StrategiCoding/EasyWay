
namespace EasyWay.Samples
{
    internal sealed class SampleCommandHandler : ICommandHandler<SampleCommand>
    {
        private readonly ICancellationTokenProvider _tokenProvider;
        private readonly IEventPublisher _eventPublisher;

        public SampleCommandHandler(
            ICancellationTokenProvider tokenProvider,
            IEventPublisher eventPublisher)
        {
            _tokenProvider = tokenProvider;
            _eventPublisher = eventPublisher;
        }

        public async Task Handle(SampleCommand command)
        {
            var token = _tokenProvider.Token;

            await _eventPublisher.Publish(new SampleEvent());
        }
    }
}
