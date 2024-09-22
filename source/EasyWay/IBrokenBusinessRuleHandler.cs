namespace EasyWay
{
    public interface IBrokenBusinessRuleHandler<TBrokenBusinessRule>
        where TBrokenBusinessRule : BusinessRule
    {
        Task Handle(TBrokenBusinessRule brokenBusinessRule);
    }
}
