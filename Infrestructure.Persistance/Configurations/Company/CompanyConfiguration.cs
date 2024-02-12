using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrestructure.Persistance.Configurations.Company
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Core.Domain.Entities.Company>
    {
        public void Configure(EntityTypeBuilder<Core.Domain.Entities.Company> builder)
        {
            //schema
            builder.ToTable("Company","Companies");
            //key
            builder.HasKey(x => x.Id);
            //relations
            builder.HasMany(x=>x.Products)
                .WithOne(c=>c.Company)
                .HasForeignKey(a=>a.CompanyId);
        }
    }
}
