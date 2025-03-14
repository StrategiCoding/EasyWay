using Microsoft.AspNetCore.Http;

namespace EasyWay.Internals
{
    internal sealed class WebApiModulExecutor<TModule> : IWebApiModulExecutor<TModule>
        where TModule : EasyWayModule
    {
        private readonly IModuleExecutor<TModule> _moduleExecutor;

        private readonly WebApiResultMapper _mapper;

        public WebApiModulExecutor(
            IModuleExecutor<TModule> moduleExecutor,
            WebApiResultMapper mapper)
        {
            _moduleExecutor = moduleExecutor;
            _mapper = mapper;
        }

        public async Task<IResult> Command<TCommand>(TCommand command, CancellationToken cancellationToken = default) 
            where TCommand : Command
        {
            var commandResult = await _moduleExecutor.Command(command, cancellationToken);

            return _mapper.Map(commandResult);
        }

        public async Task<IResult> Command<TCommand, TOperationResult>(TCommand command, CancellationToken cancellationToken = default)
            where TCommand : Command<TOperationResult>
            where TOperationResult : OperationResult
        {
            var commandResult = await _moduleExecutor.Command<TCommand, TOperationResult>(command, cancellationToken);

            return _mapper.Map(commandResult);
        }

        public async Task<IResult> Query<TQuery, TReadModel>(TQuery query, CancellationToken cancellationToken = default)
            where TQuery : Query<TReadModel>
            where TReadModel : ReadModel
        {
            var commandResult = await _moduleExecutor.Query<TQuery, TReadModel>(query, cancellationToken);

            return _mapper.Map(commandResult);
        }
    }
}
