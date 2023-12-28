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


        //Add DbContext Dependency Method
        public static IServiceCollection AddDbContextServices(this IServiceCollection Services)
        {

            Services.AddScoped<IApplicationDataContext, ApplicationDataContext>();

            Services.AddDbContext<ApplicationDataContext>(options =>
            {
                options.UseSqlServer("Server=.;Initial Catalog=Clean_ArchitectureDB;Integrated Security=true;TrustServerCertificate=True;");
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
