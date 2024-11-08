namespace EasyWay.Samples.Commands.WithResult
{
    public class SampleCommandWithResult : Command<SampleModule, SampleCommandResult>
    {
        public string Name { get; init; }
    }
}
