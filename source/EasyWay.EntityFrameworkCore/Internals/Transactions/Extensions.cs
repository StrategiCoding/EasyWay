using EasyWay.Internals.Commands;
using EasyWay.Internals.Transactions.Policies;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Transactions
{
    internal static class Extensions
    {
        internal static IServiceCollection AddTransactions(this IServiceCollection services)
        {
            services.AddScoped<ITransaction, EntityFrameworkTransaction>();

            services.AddScoped<IEntityFrameworkTransactionPolicy, MultipleDbContextWithChangesTransactionPolicy>();
            services.AddScoped<IEntityFrameworkTransactionPolicy, OneDbContextWithChangesTransactionPolicy>();
            services.AddScoped<IEntityFrameworkTransactionPolicy, NoDbContextWithChangesTransactionPolicy>();

            return services;
        }
    }
}
