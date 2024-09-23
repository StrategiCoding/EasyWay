﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace EasyWay.Internals.Commands
{
    internal static class Extensions
    {
        internal static IServiceCollection AddCommands(this IServiceCollection services, params Assembly[] assemblies)
        {
            services.AddScoped<ICommandExecutor, CommandExecutor>();

            services.Scan(s => s.FromAssemblies(assemblies)
            .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            return services;
        }
    }
}