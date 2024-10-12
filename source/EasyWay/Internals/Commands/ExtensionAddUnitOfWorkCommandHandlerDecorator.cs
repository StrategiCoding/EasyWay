using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Commands
{
    internal static class ExtensionAddUnitOfWorkCommandHandlerDecorator
    {
        internal static IServiceCollection AddUnitOfWorkCommandHandlerDecorator(this IServiceCollection services) 
        {
            services.TryDecorate(typeof(ICommandHandler<,>), typeof(UnitOfWorkCommandHandlerDecorator<,>));

            return services;
        }
    }
}
