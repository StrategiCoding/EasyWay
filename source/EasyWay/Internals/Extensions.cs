using EasyWay.Internals.AggregateRoots;
using EasyWay.Internals.Assemblies;
using EasyWay.Internals.Clocks;
using EasyWay.Internals.Commands;
using EasyWay.Internals.Contexts;
using EasyWay.Internals.DomainEvents;
using EasyWay.Internals.DomainServices;
using EasyWay.Internals.Factories;
using EasyWay.Internals.Initializers;
using EasyWay.Internals.Loggers;
using EasyWay.Internals.Policies;
using EasyWay.Internals.Queries;
using EasyWay.Internals.Repositories;
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
                .AddAssemblies(assemblies)
                .AddClocks()
                .AddContexts()
                .AddLoggers(moduleType)
                .AddAggregateRoots()
                .AddCommands(moduleType, assemblies)
                .AddQueries(assemblies)
                .AddDomainEvents(assemblies)
                .AddRepositories(assemblies)
                .AddPolicies(assemblies)
                .AddDomainServices(assemblies)
                .AddFactories(assemblies)
                .AddInitializers(assemblies);
        }

        internal static IServiceCollection AddAsBasedType(
            this IServiceCollection services,
            Type baseType,
            ServiceLifetime serviceLifetime,
            IEnumerable<Assembly> assemblies)
        {
            var typesFromAsseblies = assemblies.SelectMany(x => x.GetTypes());

            IEnumerable<Type> expectedTypes;

            if (baseType.IsGenericType)
            {
                expectedTypes = typesFromAsseblies
                .Where(x => x.IsClass && x.BaseType != null && x.BaseType.IsGenericType && x.BaseType.GetGenericTypeDefinition() == baseType);
            }
            else
            {
                expectedTypes = typesFromAsseblies
                .Where(x => x.IsClass && x.BaseType != null && x.BaseType == baseType);
            }

            expectedTypes = expectedTypes.Distinct();

            foreach (var type in expectedTypes)
            {
                services.Add(new ServiceDescriptor(type.BaseType, type, serviceLifetime));
            }

            return services;
        }

        internal static IServiceCollection AddSelfOnBasedType(
            this IServiceCollection services,
            Type baseType,
            ServiceLifetime serviceLifetime,
            IEnumerable<Assembly> assemblies)
        {
            var typesFromAsseblies = assemblies.SelectMany(x => x.GetTypes());

            IEnumerable<Type> expectedTypes;

            if (baseType.IsGenericType)
            {
                expectedTypes = typesFromAsseblies
                .Where(x => x.IsClass && x.BaseType != null && x.BaseType.IsGenericType && x.BaseType.GetGenericTypeDefinition() == baseType);
            }
            else
            {
                expectedTypes = typesFromAsseblies
                .Where(x => x.IsClass && x.BaseType != null && x.BaseType == baseType);
            }

            expectedTypes = expectedTypes.Distinct();

            foreach (var type in expectedTypes)
            {
                services.Add(new ServiceDescriptor(type, type, serviceLifetime));
            }

            return services;
        }

        internal static IServiceCollection AddAsImplementedInterfaces(
            this IServiceCollection services,
            Type interfaceType,
            ServiceLifetime serviceLifetime,
            IEnumerable<Assembly> assemblies)
        {
            var typesFromAsseblies = assemblies.SelectMany(x => x.GetTypes());

            IEnumerable<Type> expectedTypes;

            if (interfaceType.IsGenericType)
            {
                expectedTypes = typesFromAsseblies
                .Where(x => x.IsClass && x.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == interfaceType));
            }
            else
            {
                expectedTypes = typesFromAsseblies
                .Where(x => x.IsClass && x.GetInterfaces().Any(x => x == interfaceType));
            }

            expectedTypes = expectedTypes.Distinct();

            foreach (var type in expectedTypes)
            {
                
                if (interfaceType.IsGenericType)
                {
                    var interfaces = type.GetInterfaces().Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == interfaceType);

                    foreach(var @interface in interfaces)
                    {
                        services.Add(new ServiceDescriptor(@interface, type, serviceLifetime));
                    }
                }
                else
                {
                    var interfaces = type.GetInterfaces();

                    foreach (var @interface in interfaces)
                    {
                        services.Add(new ServiceDescriptor(@interface, type, serviceLifetime));
                    }
                }
            }

            return services;
        }
    }
}
