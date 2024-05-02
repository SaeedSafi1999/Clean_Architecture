using Application.Cqrs.Queris;
using Core.Application.Common;
using Core.Application.Mediator.PeriodTransactions.DTOs;
using Core.Domain.DTOs.Shared;
using Core.Domain.Entities.PeriodTransactions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mediator.PeriodTransactions.Query
{
    public class GetPeriodTtransactionByIdQuery:IQuery<ServiceRespnse<PeriodTransactionDTO>>
    {
        public long Id { get; set; }

        public class GetPeriodTtransactionByIdQueryHandler : IQueryHandler<GetPeriodTtransactionByIdQuery, ServiceRespnse<PeriodTransactionDTO>>
        {
            private readonly IGenericRepository<PeriodTransaction> _periodicRepo;

            public GetPeriodTtransactionByIdQueryHandler(IGenericRepository<PeriodTransaction> periodicRepo)
            {
                _periodicRepo = periodicRepo;
            }
            public async Task<ServiceRespnse<PeriodTransactionDTO>> Handle(GetPeriodTtransactionByIdQuery request, CancellationToken cancellationToken)
            {
                var exist = await _periodicRepo.GetAsNoTrackingQuery().Where(z => z.Id == request.Id).Select(z=>new PeriodTransactionDTO
                {
                    Amount = z.Amount,
                    Description = z.Description,
                    Id = z.Id,
                    PayDate = z.PayDate,
                    RemindMe = z.RemindMe,
                    TotalAmount = z.TotalAmount,
                    TransactionType = z.TransactionType,
                    Type = z.Type
                }).FirstOrDefaultAsync();
                if (exist is null)
                {
                    Hashtable errors = new();
                    errors.Add("Id", "Not Fpound");
                    return new ServiceRespnse<PeriodTransactionDTO>().Failed(System.Net.HttpStatusCode.NotFound, errors);
                }
                return new ServiceRespnse<PeriodTransactionDTO>().OK(exist);
            }
        }
    }
}
