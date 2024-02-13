using Application.Cqrs.Commands;
using Core.Application.Database;
using Core.Domain.DTOs.Company;
using Core.Domain.DTOs.Shared;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Core.Application.Requests.Company.Commands
{
    public class AddCompanyCommand : ICommand<ServiceRespnse<bool>>
    {
        public AddCompanyDTO addCompanyDTO { get; set; }

        public class Handle : ICommandHandler<AddCompanyCommand, ServiceRespnse<bool>>
        {
            private readonly IUnitOfWork _unitofwork;

            public Handle(IUnitOfWork unitofwork)
            {
                _unitofwork = unitofwork;
            }

            async Task<ServiceRespnse<bool>> IRequestHandler<AddCompanyCommand, ServiceRespnse<bool>>.Handle(AddCompanyCommand request, CancellationToken cancellationToken)
            {
                ServiceRespnse<bool> response = new() { IsSuccess = true };
                try
                {
                    var repo = _unitofwork.GetRepository<Core.Domain.Entities.Company>();
                    var InsertResult = await repo.AddAsync(new Domain.Entities.Company
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
