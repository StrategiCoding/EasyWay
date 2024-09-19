namespace EasyWay.Samples.Domain
{
    public class SampleBusinessRule : BusinessRule
    {
        protected override string Message => "Sample exception";

        protected override bool IsFulfilled()
        {
            return false;
        }
    }
}
