using EasyWay.Internals.UnitOfWorks.Policies;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.UnitOfWorks
{
    internal static class Extensions
    {
        internal static IServiceCollection AddUnitOfWork(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, EntityFrameworkUnitOfWork>();

            services.AddScoped<IEntityFrameworkUnitOfWorkPolicy, MultipleDbContextWithChangesUnitOfWorkPolicy>();
            services.AddScoped<IEntityFrameworkUnitOfWorkPolicy, OneDbContextWithChangesUnitOfWorkPolicy>();
            services.AddScoped<IEntityFrameworkUnitOfWorkPolicy, NoDbContextWithChangesUnitOfWorkPolicy>();

            return services;
        }
    }
}
