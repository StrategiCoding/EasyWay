
using EasyWay.Samples.Databases;

namespace EasyWay.Samples
{
    public sealed class DatabaseInitializer : IInitializer
    {
        private readonly IServiceProvider _serviceProvider;

        public DatabaseInitializer(IServiceProvider serviceProvider)
        { 
            _serviceProvider = serviceProvider;
        }

        public Task Initialize()
        {
            _serviceProvider.GetRequiredService<SampleDbContext>().Database.EnsureCreated();

            return Task.CompletedTask;
        }
    }
}
