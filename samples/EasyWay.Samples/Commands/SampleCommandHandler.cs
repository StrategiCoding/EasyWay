using EasyWay.Samples.Domain;
using EasyWay.Samples.Domain.Policies;

namespace EasyWay.Samples.Commands
{
    internal sealed class SampleCommandHandler : ICommandHandler<SampleCommand>
    {
        private readonly ICancellationContext _cancellationContext;

        private readonly ISampleAggragateRootRepository _repository;

        private readonly SampleAggregateRootFactory _factory;

        private readonly SampleDomainService _domainService;

        private readonly IConcurrencyConflictValidator _concurrencyTokenValidator;

        private readonly IEnumerable<ISamplePolicy> _policies;

        private readonly IUserContext _userContext;

        public SampleCommandHandler(
            ICancellationContext cancellationContext,
            ISampleAggragateRootRepository repository,
            SampleAggregateRootFactory factory,
            SampleDomainService domainService,
            IConcurrencyConflictValidator concurrencyTokenValidator,
            IEnumerable<ISamplePolicy> policies,
            IUserContext userContext)
        {
            _cancellationContext = cancellationContext;
            _repository = repository;
            _factory = factory;
            _domainService = domainService;
            _concurrencyTokenValidator = concurrencyTokenValidator;
            _policies = policies;
            _userContext = userContext;
        }

        public async Task<CommandResult> Handle(SampleCommand command)
        {
            var userId = _userContext.UserId;

            var token = _cancellationContext.Token;

            var data = _policies
                .Where(x => x.IsApplicable(true))
                .Single()
                .Execute("DATA");

            var x = _factory.Create();

            //_concurrencyTokenValidator.Validate(x, command);

            await _repository.Add(x);

            return CommandResult.Ok;
        }
    }
}
