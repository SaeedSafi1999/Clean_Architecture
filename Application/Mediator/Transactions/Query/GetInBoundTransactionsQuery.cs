using Application.Cqrs.Queris;
using Core.Application.Common;
using Core.Application.Mediator.Transactions.DTOs;
using Core.Domain.DTOs.Shared;
using Core.Domain.Entities.Transactions;
using Microsoft.EntityFrameworkCore;
using System.Xml.Schema;

namespace Core.Application.Mediator.Transactions.Query
{
    public class GetInBoundTransactionsQuery : IQuery<ServiceRespnse<List<TransactionDTO>>>
    {
        public int PageNumber { get; set; }
        public int Count { get; set; }
        public long TelegramId { get; set; }

        public class GetInBoundTransactionsQueryHandler : IQueryHandler<GetInBoundTransactionsQuery, ServiceRespnse<List<TransactionDTO>>>
        {
            private readonly IGenericRepository<Transaction> _transactionRepository;

            public GetInBoundTransactionsQueryHandler(IGenericRepository<Transaction> transactionRepository)
            {
                _transactionRepository = transactionRepository;
            }

            public async Task<ServiceRespnse<List<TransactionDTO>>> Handle(GetInBoundTransactionsQuery request, CancellationToken cancellationToken)
            {
                var repo = _transactionRepository.GetAsNoTrackingQuery().Where(z => z.TelegramId == request.TelegramId && z.Type == Domain.Enums.TransactionType.InBound);
                var data = await repo.Select(z => new TransactionDTO
                {
                    Type = z.Type,
                    TelegramId = z.TelegramId,
                    Amount = z.Amount,
                    Description = z.Description,
                    Id = z.Id
                }).Skip((request.PageNumber - 1) * request.Count).Take(request.Count).ToListAsync();
                var total = await repo.CountAsync();
                return new ServiceRespnse<List<TransactionDTO>>().OK(data, total);
            }
        }
    }
}
