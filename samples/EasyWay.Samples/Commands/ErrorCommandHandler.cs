using EasyWay.Samples.Domain;

namespace EasyWay.Samples.Commands
{
    public class ErrorCommandHandler : ICommandHandler<ErrorCommand>
    {
        public Task<CommandResult> Handle(ErrorCommand command)
        {
            var x = new SampleAggregateRoot();

            x.SampleMethod();

            return Task.FromResult(CommandResult.Ok);
        }
    }
}
