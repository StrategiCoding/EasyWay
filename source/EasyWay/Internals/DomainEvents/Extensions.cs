﻿using EasyWay.Internals.DomainEvents.AggragateRootIds;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyWay.Internals.DomainEvents
{
    internal static class Extensions
    {
        internal static IServiceCollection AddDomainEvents(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            services.AddScoped<IDomainEventPublisher, DomainEventPublisher>();
            services.AddScoped<IDomainEventBulkPublisher, DomainEventBulkPublisher>();
            services.AddScoped<IDomainEventContextDispacher, DomainEventContextDispacher>();

            services.AddScoped<AggrageteRootIdSearcher>();

            services.AddScoped<IDomainEventsAccessor, DomainEventsAccessor>();

            services.AddAsBasedType(typeof(DomainEventHandler<>), ServiceLifetime.Scoped, assemblies);

            RelationshipsBetweenEntityAndAggragaeRoot relation = EntityStructureInDomain.Get(assemblies);

            services.AddSingleton(relation);

            return services;
        }
    }
}
