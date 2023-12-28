using Core.Application.Database;
using Core.Domain.DTOs.Company;
using Core.Domain.DTOs.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Requests.Company.Commands
{
    public class AddCompanyCommand : IRequest<ServiceRespnse<bool>>
    {
        public AddCompanyDTO addCompanyDTO { get; set; }

        public class Handle : IRequestHandler<AddCompanyCommand, ServiceRespnse<bool>>
        {
            private readonly IUnitOfWork _unitofwork;

            public Handle(IUnitOfWork unitofwork)
            {
                _unitofwork = unitofwork;
            }

            async Task<ServiceRespnse<bool>> IRequestHandler<AddCompanyCommand, ServiceRespnse<bool>>.Handle(AddCompanyCommand request, CancellationToken cancellationToken)
            {
                ServiceRespnse<bool> response = new() { IsSuccess=true};
                try
                {
                   var InsertResult = await _unitofwork.CompanyRepository.AddAsync(new Domain.Entities.Company
                    {
                        Description = request.addCompanyDTO.Description,
                        Name = request.addCompanyDTO.Name,
                    });
                    await _unitofwork.CommitAsync();

                    response.Data = InsertResult;
                    return response;
                }
                catch (Exception ex)
                {

                    return new ServiceRespnse<bool>
                    {
                        IsSuccess = false,
                        Message = ex.Message,
                    };
                }
               
            }
        }
    }
}
