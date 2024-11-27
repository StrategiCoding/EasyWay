using EasyWay.Samples.Databases;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EasyWay.Samples
{
    public sealed class SampleModule : EasyWayModule
    {
        protected override IEnumerable<Assembly> Assemblies => new List<Assembly>
        {
            typeof(SampleModule).Assembly
        };

        protected override void ConfigureDependencies(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("Database");

            services.AddFluentValidation(Assemblies);

            services.AddEntityFrameworkCore<SampleDbContext>(x => x.UseNpgsql(connectionString));
        }
    }
}
