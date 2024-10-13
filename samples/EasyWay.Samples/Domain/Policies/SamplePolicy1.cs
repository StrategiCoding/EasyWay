namespace EasyWay.Samples.Domain.Policies
{
    public sealed class SamplePolicy1 : ISamplePolicy
    {
        public string Execute(string data)
        {
            return data;
        }

        public bool IsApplicable(bool data) => true;
    }
}
