using Application.Cqrs.Queris;
using Core.Application.Common;
using Core.Application.Mediator.Banks.DTOs;
using Core.Domain.DTOs.Shared;
using Core.Domain.Entities.Banks;
using Microsoft.EntityFrameworkCore;

namespace Core.Application.Mediator.Banks.Query
{
    public class GetAllBankQuery:IQuery<ServiceRespnse<List<BankDTO>>>
    {
        public int PageNnumber { get; set; }
        public int Count { get; set; }

        public class GetAllBankQueryHandler : IQueryHandler<GetAllBankQuery, ServiceRespnse<List<BankDTO>>>
        {
            private readonly IGenericRepository<Bank> _bankRepository;

            public GetAllBankQueryHandler(IGenericRepository<Bank> bankRepository)
            {
                _bankRepository = bankRepository;
            }

            public async Task<ServiceRespnse<List<BankDTO>>> Handle(GetAllBankQuery request, CancellationToken cancellationToken)
            {
                var repo = _bankRepository.GetAsNoTrackingQuery();
                //TODO:set filters here

                var banks = await repo.Select(z => new BankDTO
                {
                    Branch = z.Branch,
                    Id = z.Id,
                    Name = z.Name,
                    SVG = z.SVG
                }).Skip((request.PageNnumber - 1) * request.Count).Take(request.Count).ToListAsync();
                var totalCounts = await repo.CountAsync();
                return new ServiceRespnse<List<BankDTO>>().OK(banks,total:totalCounts);
            }
        }
    }
}
