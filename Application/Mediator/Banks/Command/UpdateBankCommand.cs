using Application.Cqrs.Commands;
using Core.Application.Common;
using Core.Application.Mediator.Banks.DTOs;
using Core.Domain.DTOs.Shared;
using Core.Domain.Entities.Banks;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace Core.Application.Mediator.Banks.Command
{
    public class UpdateBankCommand:ICommand<ServiceRespnse>
    {
        public BankDTO dto { get; set; }

        public class UpdateBankCommandHandler : ICommandHandler<UpdateBankCommand, ServiceRespnse>
        {
            private readonly IGenericRepository<Bank> _bankrepository;

            public UpdateBankCommandHandler(IGenericRepository<Bank> bankrepository)
            {
                _bankrepository = bankrepository;
            }

            public async Task<ServiceRespnse> Handle(UpdateBankCommand request, CancellationToken cancellationToken)
            {
                var bank = await _bankrepository.GetQuery().Where(x => x.Id == request.dto.Id).FirstOrDefaultAsync();
                if(bank is null)
                {
                    Hashtable errors = new();
                    errors.Add("Id","not found");
                    return new ServiceRespnse().Failed(System.Net.HttpStatusCode.NotFound,errors);
                }
                bank.Branch = request.dto.Branch;
                bank.SVG = request.dto.SVG;
                bank.Name = request.dto.Name;

                await _bankrepository.UpdateAsync(bank, cancellationToken);
                return new ServiceRespnse().OK();
                    
            }
        }
    }
}
