using EasyWay.Internals.DomainEvents;
using EasyWay.Internals.Entities;
using EasyWay.Internals.Repositories;
using EasyWay.Internals.Transactions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay
{
    public static class Extnsions
    {
        public static void AddEntityFrameworkCore<TContext>(
            this IServiceCollection services,
            Action<DbContextOptionsBuilder> optionsAction)
            where TContext : DbContext
        {
            services.AddDbContextPool<TContext>(optionsAction);

            services.AddScoped((Func<IServiceProvider, DbContext>)(sp => sp.GetRequiredService<TContext>()));

            services.AddScoped<IEntitiesContext, EntityFrameworkEntitiesContext>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddTransactions();
        }
    }
}
