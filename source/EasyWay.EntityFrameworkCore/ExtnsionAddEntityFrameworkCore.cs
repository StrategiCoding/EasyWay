using EasyWay.EntityFrameworkCore.Internals.DomainEvents;
using EasyWay.EntityFrameworkCore.Internals.Repositories;
using EasyWay.Internals.DomainEvents;
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

            services.AddTransient<IDomainEventsAccessor, DomainEventsAccessor>();

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
