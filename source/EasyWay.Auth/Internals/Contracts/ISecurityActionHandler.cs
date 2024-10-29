using EasyWay.Internals.Domain.SeedWorks.Results;

namespace EasyWay.Internals.Contracts
{
    internal interface ISecurityActionHandler<TAction>
        where TAction : class, ISecurityAction
    {
        Task<SecurityResult> Handle(TAction action);
    }

    internal interface ISecurityActionHandler<TAction, TResult>
        where TAction : class, ISecurityAction<TResult>
    {
        Task<SecurityResult<TResult>> Handle(TAction action);
    }
}
