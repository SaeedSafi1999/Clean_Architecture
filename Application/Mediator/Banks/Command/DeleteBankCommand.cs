using Application.Cqrs.Commands;
using Core.Application.Common;
using Core.Domain.DTOs.Shared;
using Core.Domain.Entities.Banks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Core.Application.Mediator.Banks.Command
{
    public class DeleteBankCommand:ICommand<ServiceRespnse>
    {
        public long Id { get; set; }

        public class DeleteBankCommandHandler : ICommandHandler<DeleteBankCommand, ServiceRespnse>
        {
            private readonly IGenericRepository<Bank> _bankrepository;

            public DeleteBankCommandHandler(IGenericRepository<Bank> bankrepository)
            {
                _bankrepository = bankrepository;
            }

            public async Task<ServiceRespnse> Handle(DeleteBankCommand request, CancellationToken cancellationToken)
            {
                var bank = await _bankrepository.GetQuery().Where(x => x.Id == request.Id).FirstOrDefaultAsync();
                if (bank is null)
                {
                    Hashtable errors = new();
                    errors.Add("Id", "not found");
                    return new ServiceRespnse().Failed(System.Net.HttpStatusCode.NotFound, errors);
                }
                await _bankrepository.SoftDeleteAsync(bank,cancellationToken);

                return new ServiceRespnse().OK();
            }
        }
    }
}
