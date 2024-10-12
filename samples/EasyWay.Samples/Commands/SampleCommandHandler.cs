using EasyWay.Samples.Domain;

namespace EasyWay.Samples.Commands
{
    internal sealed class SampleCommandHandler : ICommandHandler<SampleModule, SampleCommand>
    {
        private readonly ICancellationContext _cancellationContext;

        private readonly ISampleAggragateRootRepository _repository;

        private readonly SampleAggregateRootFactory _factory;

        private readonly SampleDomainService _domainService;

        public SampleCommandHandler(
            ICancellationContext cancellationContext,
            ISampleAggragateRootRepository repository,
            SampleAggregateRootFactory factory,
            SampleDomainService domainService)
        {
            _cancellationContext = cancellationContext;
            _repository = repository;
            _factory = factory;
            _domainService = domainService;
        }

        public async Task Handle(SampleCommand command)
        {
            var token = _cancellationContext.Token;

            var x = _factory.Create();

            await _repository.Add(x);
        }
    }
}
