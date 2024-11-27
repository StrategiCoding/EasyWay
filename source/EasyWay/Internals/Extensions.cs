using EasyWay.Internals.AggregateRoots;
using EasyWay.Internals.Commands;
using EasyWay.Internals.Contexts;
using EasyWay.Internals.DomainEvents;
using EasyWay.Internals.DomainServices;
using EasyWay.Internals.Factories;
using EasyWay.Internals.Initializers;
using EasyWay.Internals.Policies;
using EasyWay.Internals.Queries;
using EasyWay.Internals.Repositories;
using EasyWay.Internals.Validation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyWay.Internals
{
    internal static class Extensions
    {
        internal static void AddEasyWay(
            this IServiceCollection services,
            Type moduleType,
            IEnumerable<Assembly> assemblies)
        {
            services
                .AddContexts()
                .AddAggregateRoots()
                .AddCommands(moduleType, assemblies)
                .AddQueries(moduleType, assemblies)
                .AddDomainEvents(assemblies)
                .AddRepositories(assemblies)
                .AddPolicies(assemblies)
                .AddDomainServices(assemblies)
                .AddFactories(assemblies)
                .AddInitializers(assemblies);
        }
    }
}
