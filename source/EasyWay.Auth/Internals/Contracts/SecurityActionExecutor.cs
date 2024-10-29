using EasyWay.Internals.Domain.SeedWorks.Results;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyWay.Internals.Contracts
{
    internal sealed class SecurityActionExecutor : ISecurityActionExecutor
    {
        private readonly IServiceProvider _serviceProvider;

        public SecurityActionExecutor(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<SecurityResult> Execute<TAction>(TAction action)
            where TAction : class, ISecurityAction
        {
            var result = await _serviceProvider.GetRequiredService<ISecurityActionHandler<TAction>>().Handle(action);

            return result;
        }

        public async Task<SecurityResult<TResult>> Execute<TAction, TResult>(TAction action)
            where TAction : class, ISecurityAction<TResult>
        {
            var result = await _serviceProvider.GetRequiredService<ISecurityActionHandler<TAction,TResult>>().Handle(action);

            return result;
        }
    }
}
