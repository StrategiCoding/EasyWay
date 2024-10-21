namespace EasyWay.Samples.Commands.WithResult
{
    public sealed class CreateJwtCommandHandler : ICommandHandler<SampleModule, CreateJwtCommand, Jwt>
    {
        private readonly IJwtFactory _jwtFactory;

        public CreateJwtCommandHandler(IJwtFactory jwtFactory)
        {
            _jwtFactory = jwtFactory;
        }

        public Task<Jwt> Handle(CreateJwtCommand query)
        {
            var token = _jwtFactory.Create(Guid.NewGuid(), "ROLE");

            return Task.FromResult(new Jwt()
            {
                Token = token,
            });
        }
    }
}
