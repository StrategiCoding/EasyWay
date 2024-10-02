using System.Reflection;

namespace EasyWay.Samples
{
    public sealed class SampleModule : Module
    {
        protected override IEnumerable<Assembly> Assemblies => new List<Assembly> 
        { 
            typeof(SampleModule).Assembly 
        };

        protected override void ConfigureDependencies(IServiceCollection services)
        {
            //throw new NotImplementedException();
        }
    }
}
