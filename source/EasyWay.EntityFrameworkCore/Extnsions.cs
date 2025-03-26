using EasyWay.Internals.AggregateRoots;
using EasyWay.Internals.DomainEvents;
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

            services.AddScoped<IAggregateRootsContext, EntityFrameworkAggregateRootsContext>();
            services.AddTransient<IDomainEventsContext, DomainEventsAccessor>();

            services.AddScoped(typeof(IWriteGenericRepository<>), typeof(WriteGenericRepository<>));

            services.AddTransactions();
        }
    }
}
