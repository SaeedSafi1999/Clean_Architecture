using Core.Domain.Entities.Transactions;
using Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mediator.Transactions.DTOs
{
    public class TransactionDTO
    {
        public long Id { get; set; }
        public decimal Amount { get; set; }
        public string? Description { get; set; }
        public long TelegramId { get; set; }
        public TransactionType? Type { get; set; }
    }
}
