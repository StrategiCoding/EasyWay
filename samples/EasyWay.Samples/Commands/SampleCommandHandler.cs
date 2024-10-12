using EasyWay.Samples.Domain;

namespace EasyWay.Samples.Commands
{
    internal sealed class SampleCommandHandler : ICommandHandler<SampleModule, SampleCommand>
    {
        private readonly ICancellationContext _cancellationContext;

        private readonly ISampleAggragateRootRepository _repository;

        private readonly SampleAggregateRootFactory _factory;

        private readonly SampleDomainService _domainService;

        private readonly IConcurrencyConflictValidator _concurrencyTokenValidator;

        public SampleCommandHandler(
            ICancellationContext cancellationContext,
            ISampleAggragateRootRepository repository,
            SampleAggregateRootFactory factory,
            SampleDomainService domainService,
            IConcurrencyConflictValidator concurrencyTokenValidator)
        {
            _cancellationContext = cancellationContext;
            _repository = repository;
            _factory = factory;
            _domainService = domainService;
            _concurrencyTokenValidator = concurrencyTokenValidator;
        }

        public async Task Handle(SampleCommand command)
        {
            var token = _cancellationContext.Token;

            var x = _factory.Create();

            _concurrencyTokenValidator.Validate(x, command);

            await _repository.Add(x);
        }
    }
}
