using EasyWay.EntityFrameworkCore;
using EasyWay.Samples.Domain;

namespace EasyWay.Samples.Commands
{
    internal sealed class SampleCommandHandler : ICommandHandler<SampleCommand>
    {
        private readonly ICancellationTokenProvider _tokenProvider;

        private readonly IGenericRepository<SampleAggragete> _repository;

        public SampleCommandHandler(
            ICancellationTokenProvider tokenProvider,
            IGenericRepository<SampleAggragete> repository)
        {
            _tokenProvider = tokenProvider;
            _repository = repository;
        }

        public async Task Handle(SampleCommand command)
        {
            var token = _tokenProvider.Token;

            var x = new SampleAggragete();

            await _repository.Add(x);
        }
    }
}
