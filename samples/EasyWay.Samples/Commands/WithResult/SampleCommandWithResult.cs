namespace EasyWay.Samples.Commands.WithResult
{
    public class SampleCommandWithResult : Command<SampleCommandResult>
    {
        public string Name { get; init; }
    }
}
