using Application.Cqrs.Queris;
using Core.Application.Common;
using Core.Application.Mediator.PeriodTransactions.DTOs;
using Core.Domain.DTOs.Shared;
using Core.Domain.Entities.PeriodTransactions;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Mediator.PeriodTransactions.Query
{
    public class GetAllPeriodTransactionQuery:IQuery<ServiceRespnse<List<PeriodTransactionDTO>>>
    {
        public int PageNumber { get; set; }
        public int Count { get; set; }
        public long TelegramId { get; set; }

        public class GetAllPeriodTransactionQueryHandler : IQueryHandler<GetAllPeriodTransactionQuery, ServiceRespnse<List<PeriodTransactionDTO>>>
        {
            private readonly IGenericRepository<PeriodTransaction> _periodicRepo;

            public GetAllPeriodTransactionQueryHandler(IGenericRepository<PeriodTransaction> periodicRepo)
            {
                _periodicRepo = periodicRepo;
            }
            public async Task<ServiceRespnse<List<PeriodTransactionDTO>>> Handle(GetAllPeriodTransactionQuery request, CancellationToken cancellationToken)
            {
                var repo = _periodicRepo.GetAsNoTrackingQuery().Where(z => z.TelegramId == request.TelegramId);
                var data = await repo.Select(z=>new PeriodTransactionDTO
                {
                    Amount = z.Amount,
                    Description = z.Description,
                    Id = z.Id,
                    PayDate = z.PayDate,
                    RemindMe = z.RemindMe,
                    TotalAmount = z.TotalAmount,
                    TransactionType = z.TransactionType,
                    Type = z.Type
                }).Skip((request.PageNumber -1)*request.Count).Take(request.Count).ToListAsync();
                var total = await repo.CountAsync();
                return new ServiceRespnse<List<PeriodTransactionDTO>>().OK(data, total);
            }
        }
    }
}
