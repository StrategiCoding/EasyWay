namespace EasyWay.Samples.Domain.Policies
{
    public sealed class SamplePolicy2 : ISamplePolicy
    {
        public string Execute(string data)
        {
            return data;
        }

        public bool IsApplicable(bool data) => false;
    }
}
