using Core.Application.Common;
using Core.Application.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrestructure.Persistance.Database.DependencyInjection
{
    public static class DatabaseDependencyInjection
    {
        //add generic repository Dependency Method
        public static IServiceCollection AddRepositoriesDependency(this IServiceCollection Services)
        {
           return Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }

        //Add DbContext Dependency Method
        public static IServiceCollection AddDbContextServices(this IServiceCollection Services)
        {

            Services.AddScoped<IApplicationDataContext, ApplicationDataContext>();

            Services.AddDbContext<ApplicationDataContext>(options =>
            {
                options.UseSqlServer("Server=192.168.1.20;Database=Clean_ArchitectureDB;User Id=sa;Password=Simagar@2020; MultipleActiveResultSets=True;Connection Timeout=30;TrustServerCertificate=True;");
            });

            return Services;
        }


        //Add UnitOfWork Dependency Method
        public static IServiceCollection AddUnitOfWorkServices(this IServiceCollection Services)
        {
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            return Services;
        }
    }
}
