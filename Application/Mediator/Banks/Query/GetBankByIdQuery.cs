using Application.Cqrs.Queris;
using Core.Application.Common;
using Core.Application.Mediator.Banks.DTOs;
using Core.Domain.DTOs.Shared;
using Core.Domain.Entities.Banks;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Core.Application.Mediator.Banks.Query
{
    public class GetBankByIdQuery:IQuery<ServiceRespnse<BankDTO>>
    {
        public long Id { get; set; }

        public class GetBankByIdQueryHandler : IQueryHandler<GetBankByIdQuery, ServiceRespnse<BankDTO>>
        {
            private readonly IGenericRepository<Bank> _bankRepository;

            public GetBankByIdQueryHandler(IGenericRepository<Bank> bankRepository)
            {
                _bankRepository = bankRepository;
            }
            public async Task<ServiceRespnse<BankDTO>> Handle(GetBankByIdQuery request, CancellationToken cancellationToken)
            {
                var bank = await _bankRepository.GetQuery().Where(x => x.Id == request.Id).Select(z=> new BankDTO
                {
                    Branch = z.Branch,
                    Id = z.Id,
                    Name = z.Name,
                    SVG = z.SVG
                }).FirstOrDefaultAsync();
                if (bank is null)
                {
                    Hashtable errors = new();
                    errors.Add("Id", "not found");
                    return new ServiceRespnse<BankDTO>().Failed(System.Net.HttpStatusCode.NotFound, errors);
                }
                return new ServiceRespnse<BankDTO>().OK(bank);

            }
        }
    }
}
