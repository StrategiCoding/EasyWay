using EasyWay.EntityFrameworkCore.Internals.AggregateRoots;
using EasyWay.EntityFrameworkCore.Internals.DomainEvents;
using EasyWay.EntityFrameworkCore.Internals.Repositories;
using EasyWay.EntityFrameworkCore.Internals.UnitOfWorks;
using EasyWay.Internals.AggregateRoots;
using EasyWay.Internals.DomainEvents;
using EasyWay.Internals.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.EntityFrameworkCore
{
    public static class ExtnsionAddEntityFrameworkCore
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

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            services.AddScoped<IUnitOfWork, EntityFrameworkUnitOfWork>();
        }
    }
}
