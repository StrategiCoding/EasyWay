﻿using EasyWay.Samples.Databases;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace EasyWay.Samples
{
    public class SampleModuleConfigurator : ModuleConfigurator<SampleModule>
    {
        protected override IEnumerable<Assembly> Assemblies => new List<Assembly>
        {
            typeof(SampleModule).Assembly
        };

        protected override void ConfigureDependencies(IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("Database");

            services.AddEntityFrameworkCore<SampleDbContext>(x => x.UseNpgsql(connectionString));
        }
    }
}
