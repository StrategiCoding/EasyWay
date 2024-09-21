using EasyWay.Samples.Domain;

namespace EasyWay.Samples.Commands
{
    public class ErrorCommandHandler : ICommandHandler<ErrorCommand>
    {
        public Task Handle(ErrorCommand command)
        {
            var x = new SampleAggragete();

            x.SampleMethod();

            return Task.CompletedTask;
        }
    }
}
