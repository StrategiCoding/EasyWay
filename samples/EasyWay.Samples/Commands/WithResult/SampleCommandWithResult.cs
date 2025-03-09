namespace EasyWay.Samples.Commands.WithResult
{
    public class SampleCommandWithResult : Command<SampleCommandResult>, IWithConcurrencyToken
    {
        public string Name { get; init; }

        public short ConcurrencyToken { get; init; }
    }
}
