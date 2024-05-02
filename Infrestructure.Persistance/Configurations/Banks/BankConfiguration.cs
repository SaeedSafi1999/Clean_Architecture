﻿using Core.Domain.Entities.Banks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrestructure.Persistance.Configurations.Banks
{
    public class BankConfiguration : IEntityTypeConfiguration<Bank>
    {
        public void Configure(EntityTypeBuilder<Bank> builder)
        {
            builder.ToTable("Banks","Bank");
            builder.HasMany(z => z.Transactions).WithOne(x => x.Bank).HasForeignKey(z => z.BankId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
