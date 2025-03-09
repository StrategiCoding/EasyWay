namespace EasyWay.Samples.Commands
{
    public class SampleCommand : Command
    {
        public Guid Id { get; init; }

        public short ConcurrencyToken { get; init; }
    }
}
