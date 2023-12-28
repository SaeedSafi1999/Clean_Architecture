using AutoMapper;
using Core.Application.Database;
using Core.Domain.DTOs.Product;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Requests.Product.Queries
{
    public class GetProductsOfCompanyQuery : IRequest<IEnumerable<ProductDTO>>
    {
        public int CompanyId { get; set; }


        public class Handle : IRequestHandler<GetProductsOfCompanyQuery, IEnumerable<ProductDTO>>
        {
            private readonly IUnitOfWork _unitOfWork;
            public Handle(IUnitOfWork unitOfWork)
            {
                _unitOfWork = unitOfWork;
            }

            async Task<IEnumerable<ProductDTO>> IRequestHandler<GetProductsOfCompanyQuery, IEnumerable<ProductDTO>>.Handle(GetProductsOfCompanyQuery request, CancellationToken cancellationToken)
            {
                return await _unitOfWork.ProductRepository.GetProductsOfCompany(request.CompanyId);

            }
        }
    }
}
