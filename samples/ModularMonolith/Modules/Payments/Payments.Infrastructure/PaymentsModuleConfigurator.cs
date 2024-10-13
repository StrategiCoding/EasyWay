using EasyWay;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Payments.Application;
using Payments.Domain.Payments;
using System.Reflection;

namespace Payments.Infrastructure
{
    public sealed class PaymentsModuleConfigurator : ModuleConfigurator<PaymentsModule>
    {
        protected override IEnumerable<Assembly> Assemblies => new List<Assembly>
        {
            typeof(Payment).Assembly,
            //typeof(CreatePaymentCommand).Assembly,
            typeof(PaymentsDbContext).Assembly,
        };

        protected override void ConfigureDependencies(IServiceCollection services, IConfiguration configuration)
        {
            throw new NotImplementedException();
        }
    }
}
