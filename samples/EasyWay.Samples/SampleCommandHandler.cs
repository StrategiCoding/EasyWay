
namespace EasyWay.Samples
{
    internal sealed class SampleCommandHandler : ICommandHandler<SampleCommand>
    {
        private readonly ICancellationTokenProvider _tokenProvider;
        private readonly IDomainEventPublisher _eventPublisher;

        public SampleCommandHandler(
            ICancellationTokenProvider tokenProvider,
            IDomainEventPublisher eventPublisher)
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
