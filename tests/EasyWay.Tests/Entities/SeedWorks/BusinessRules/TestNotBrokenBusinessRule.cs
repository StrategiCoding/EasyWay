namespace EasyWay.Tests.Entities.SeedWorks.BusinessRules
{
    internal class TestNotBrokenBusinessRule : BusinessRule
    {
        protected internal override string Message => string.Empty;

        protected internal override bool IsFulfilled() => true;
    }
}
