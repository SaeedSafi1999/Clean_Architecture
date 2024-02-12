using Core.Application.Database;
using Core.Domain.Entities;
using Entities.Users;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrestructure.Persistance.Database
{
    public class ApplicationDataContext : DbContext, IApplicationDataContext
    {

        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options):base(options)
        {
                
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
