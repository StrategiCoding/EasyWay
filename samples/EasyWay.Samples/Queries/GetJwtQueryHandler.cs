
namespace EasyWay.Samples.Queries
{
    public sealed class GetJwtQueryHandler : IQueryHandler<SampleModule, GetJwtQuery, JwtReadModel>
    {
        private readonly IJwtFactory _jwtFactory;

        public GetJwtQueryHandler(IJwtFactory jwtFactory) 
        {
            _jwtFactory = jwtFactory;
        }

        public Task<JwtReadModel> Handle(GetJwtQuery query)
        {
            var token = _jwtFactory.Create(Guid.NewGuid(), "ROLE");

            return Task.FromResult(new JwtReadModel()
            {
                Token = token,
            });
        }
    }
}
