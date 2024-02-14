using Core.Domain.Entities.Role;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrestructure.Persistance.Configurations.Role
{
    public class RoleConfiguration : IEntityTypeConfiguration<Core.Domain.Entities.Role.Role>
    {
        public void Configure(EntityTypeBuilder<Core.Domain.Entities.Role.Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(x => x.Id);
            builder.HasMany(z=>z.Users).WithOne(z=>z.Role).HasForeignKey(z=>z.RoleId);
            List<Core.Domain.Entities.Role.Role> seedData = new()
            {
                new Core.Domain.Entities.Role.Role{Id=1,RoleName="SuperAdmin"},
                new Core.Domain.Entities.Role.Role{Id=2,RoleName="Admin"},
                new Core.Domain.Entities.Role.Role{Id=3,RoleName="User"},
            }; 
            builder.HasData(seedData.ToArray());
        }
    }
}
