﻿using Common;
using Core.Application.Extensions.Mapper;
using Core.Domain.BaseEntity;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions;
using System.Reflection;
using Application.Cqrs;

namespace Core.Application.Extensions
{
    public static class ApplicationLayarDependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            //Add AutoMapper Services
            MapperInjection.AddMapperServcies(Services);

            // Add MediatR
            Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });

            //add scope transient singletone 
            var commonAssembly = typeof(SiteSettings).Assembly;
            var entitiesAssembly = typeof(IEntity).Assembly;
            var dataAssembly = typeof(AppContext).Assembly;
            var applicationAssmemly = typeof(IRequest<>).Assembly;
            Services.Scan(s =>
            s.FromAssemblies(commonAssembly, entitiesAssembly, dataAssembly, applicationAssmemly)
            .AddClasses(c => c.AssignableTo(typeof(IScopedDependency))
            ).AsImplementedInterfaces()
            .WithScopedLifetime());

            Services.Scan(s =>
            s.FromAssemblies(commonAssembly, entitiesAssembly, dataAssembly, applicationAssmemly)
            .AddClasses(c => c.AssignableTo(typeof(ITransientDependency))
            ).AsImplementedInterfaces()
            .WithScopedLifetime());

            Services.Scan(s =>
            s.FromAssemblies(commonAssembly, entitiesAssembly, dataAssembly, applicationAssmemly)
            .AddClasses(c => c.AssignableTo(typeof(ISingletonDependency))
            ).AsImplementedInterfaces()
            .WithScopedLifetime());

            //add cqrs 
            Services.AddCqrs();


            return Services;
        }
    }
}
