namespace EasyWay.Internals.BusinessRules
{
    internal interface IBrokenBusinessRuleDispacher
    {
        Task Dispach<TBrokenBusinessRule>(TBrokenBusinessRule brokenBusinessRule)
            where TBrokenBusinessRule : BusinessRule;
    }
}
