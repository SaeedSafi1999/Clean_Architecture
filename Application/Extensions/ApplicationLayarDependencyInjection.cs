using Core.Application.Extensions.Mapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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

            return Services;
        }
    }
}
