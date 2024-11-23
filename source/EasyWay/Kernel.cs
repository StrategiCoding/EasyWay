using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay
{
    public sealed class Kernel
    {
        private readonly IServiceCollection _services;

        private readonly IConfiguration _configuration;

        private readonly IList<Tuple<Type, Type>> _moduleExecutorTypes = new List<Tuple<Type, Type>>();

        private Kernel() 
        {
            _services = new ServiceCollection();

            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();
        }

        public static Kernel Create() => new Kernel();

        public void AddModule<TModule>()
            where TModule : EasyWayModule, new()
        {
            var configuration = _configuration.GetSection(typeof(TModule).Name);

            var moduleExecutorType = new TModule().Initialize(_services, configuration);

            _moduleExecutorTypes.Add(moduleExecutorType);
        }

        public async Task BuildAsync(IServiceCollection services)
        {
            var x = _services.BuildServiceProvider();

            foreach (var type in _moduleExecutorTypes)
            {
                var executor = x.GetRequiredService(type.Item1);

                services.AddSingleton(type.Item1, executor);
            }

            var initializers = x.GetServices<IInitializer>();

            foreach(var initializer in initializers)
            {
                await initializer.Initialize();
            }
        }
    }
}
