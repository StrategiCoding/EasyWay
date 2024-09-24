using EasyWay.EntityFrameworkCore;
using EasyWay.Samples.Domain;

namespace EasyWay.Samples.Commands
{
    internal sealed class SampleCommandHandler : ICommandHandler<SampleCommand>
    {
        private readonly ICancellationContext _cancellationContext;

        private readonly IGenericRepository<SampleAggregateRoot> _repository;

        public SampleCommandHandler(
            ICancellationContext cancellationContext,
            IGenericRepository<SampleAggregateRoot> repository)
        {
            _cancellationContext = cancellationContext;
            _repository = repository;
        }

        public async Task Handle(SampleCommand command)
        {
            var token = _cancellationContext.Token;

            var x = new SampleAggregateRoot();

            await _repository.Add(x);
        }
    }
}
