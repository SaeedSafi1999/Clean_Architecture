using Core.Domain.Entities.PeriodTransactions;
using Core.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrestructure.Persistance.Configurations.PeriodTransactions
{
    public class PeriodTransactionConfiguration : IEntityTypeConfiguration<PeriodTransaction>
    {
        public void Configure(EntityTypeBuilder<PeriodTransaction> builder)
        {
            builder.ToTable("PeriodTransactions", "Transaction");
        }
    }
}
