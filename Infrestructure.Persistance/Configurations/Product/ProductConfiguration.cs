using Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrestructure.Persistance.Configurations.Product
{
    public class ProductConfiguration : IEntityTypeConfiguration<Core.Domain.Entities.Product>
    {
        public void Configure(EntityTypeBuilder<Core.Domain.Entities.Product> builder)
        {
            //schema
            builder.ToTable("Product", "Products");
            //key
            builder.HasKey(z => z.Id);
            //relation
            builder.HasOne<Core.Domain.Entities.Company>(z=>z.Company)
                .WithMany(x=>x.Products)
                .HasForeignKey(x=>x.CompanyId);
        }
    }
}
