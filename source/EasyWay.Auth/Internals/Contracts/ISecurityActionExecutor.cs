using EasyWay.Internals.Domain.SeedWorks.Results;

namespace EasyWay.Internals.Contracts
{
    internal interface ISecurityActionExecutor
    {
        Task<SecurityResult> Execute<TAction>(TAction action) 
            where TAction : class, ISecurityAction;

        Task<SecurityResult<TResult>> Execute<TAction, TResult>(TAction action) 
            where TAction : class, ISecurityAction<TResult>;
    }
}
