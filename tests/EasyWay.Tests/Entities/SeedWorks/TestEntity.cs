namespace EasyWay.Tests.Entities.SeedWorks
{
    internal sealed class TestEntity : Entity
    {
        public void CheckBusinessRule(BusinessRule businessRule)
        {
            Check(businessRule);
        }
    }
}
