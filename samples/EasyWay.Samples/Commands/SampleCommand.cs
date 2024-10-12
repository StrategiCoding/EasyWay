namespace EasyWay.Samples.Commands
{
    public class SampleCommand : Command<SampleModule>, IWithConcurrencyToken
    {
        public Guid Id { get; init; }

        public short ConcurrencyToken { get; init; }
    }
}
