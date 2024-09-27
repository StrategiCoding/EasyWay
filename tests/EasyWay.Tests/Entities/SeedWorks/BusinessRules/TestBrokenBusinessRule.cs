namespace EasyWay.Tests.Entities.SeedWorks.BusinessRules
{
    internal class TestBrokenBusinessRule : BusinessRule
    {
        protected internal override string Message => string.Empty;

        protected internal override bool IsFulfilled() => false;
    }
}
