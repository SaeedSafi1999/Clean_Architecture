using Application.Cqrs.Commands;
using Application.Cqrs.Queris;
using Core.Application.Common;
using Core.Application.Mediator.Transactions.DTOs;
using Core.Domain.DTOs.Shared;
using Core.Domain.Entities.Transactions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Core.Application.Mediator.Transactions.Query
{
    public class GetTransactionByIdQuery:IQuery<ServiceRespnse<TransactionDTO>>
    {
        public long Id { get; set; }

        public class GetTransactionByIdQueryHandler : IQueryHandler<GetTransactionByIdQuery, ServiceRespnse<TransactionDTO>>
        {
            private readonly IGenericRepository<Transaction> _transactionRepository;

            public GetTransactionByIdQueryHandler(IGenericRepository<Transaction> transactionRepository)
            {
                _transactionRepository = transactionRepository;
            }

            public async Task<ServiceRespnse<TransactionDTO>> Handle(GetTransactionByIdQuery request, CancellationToken cancellationToken)
            {
                var errors = new Hashtable();
                var trans = await _transactionRepository.GetQuery().Where(z => z.Id == request.Id).Select(x=>new TransactionDTO
                {
                    Amount = x.Amount,
                    Description = x.Description,
                    Id = request.Id,
                    TelegramId = x.TelegramId,
                    Type = x.Type
                }).FirstOrDefaultAsync();
                errors.Add("id", "can not find by this id");
                if (trans is null)
                    return new ServiceRespnse<TransactionDTO>().Failed(System.Net.HttpStatusCode.NotFound, errors);
                return new ServiceRespnse<TransactionDTO>().OK(trans);

            }
        }
    }
}
