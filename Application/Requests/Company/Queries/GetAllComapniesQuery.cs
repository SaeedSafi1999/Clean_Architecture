using Core.Application.Database;
using Core.Domain.DTOs.Company;
using Core.Domain.DTOs.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Requests.Company.Queries
{
    public class GetAllComapniesQuery:IRequest<List<CompanyDTO>>
    {

        public class Handle : IRequestHandler<GetAllComapniesQuery, List<CompanyDTO>>
        {
            private readonly IUnitOfWork _unitOfWork;

            public Handle(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            async Task<List<CompanyDTO>> IRequestHandler<GetAllComapniesQuery, List<CompanyDTO>>.Handle(GetAllComapniesQuery request, CancellationToken cancellationToken)
            {
                return (await _unitOfWork.CompanyRepository.GetAllAsync(cancellationToken)).Select(x=>new CompanyDTO
                {
                    Description = x.Description,
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();
            }
        }
    }
}
