using Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mediator.PeriodTransactions.DTOs
{
    public class PeriodTransactionDTO
    {
        public long Id { get; set; }
        public DateTime PayDate { get; set; }
        public decimal Amount { get; set; }
        public bool RemindMe { get; set; }
        public decimal TotalAmount { get; set; }
        public PeriodTransactionType Type { get; set; }
        public TransactionType TransactionType { get; set; }
        public string Description { get; set; }
    }
}
