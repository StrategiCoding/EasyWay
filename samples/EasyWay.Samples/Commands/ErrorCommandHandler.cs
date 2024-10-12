using EasyWay.Samples.Domain;

namespace EasyWay.Samples.Commands
{
    public class ErrorCommandHandler : ICommandHandler<SampleModule, ErrorCommand>
    {
        public Task Handle(ErrorCommand command)
        {
            var x = new SampleAggregateRoot();

            x.SampleMethod();

            return Task.CompletedTask;
        }
    }
}
