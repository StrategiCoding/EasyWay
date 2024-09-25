﻿using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace EasyWay.Internals.DomainEvents
{
    internal static class Extensions
    {
        internal static IServiceCollection AddDomainEvents(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddScoped<IDomainEventPublisher, DomainEventPublisher>();
            services.AddScoped<IDomainEventBulkPublisher, DomainEventBulkPublisher>();
            services.AddScoped<IDomainEventDispacher, DomainEventDispacher>();

            services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(IDomainEventHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            return services;
        }
    }
}
