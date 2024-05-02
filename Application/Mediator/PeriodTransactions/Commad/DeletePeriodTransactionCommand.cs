using Application.Cqrs.Commands;
using Core.Application.Common;
using Core.Domain.DTOs.Shared;
using Core.Domain.Entities.PeriodTransactions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Core.Application.Mediator.PeriodTransactions.Commad
{
    public class DeletePeriodTransactionCommand:ICommand<ServiceRespnse>
    {
        public long Id { get; set; }

        public class DeletePeriodTransactionCommandHandler : ICommandHandler<DeletePeriodTransactionCommand, ServiceRespnse>
        {
            private readonly IGenericRepository<PeriodTransaction> _periodicRepo;

            public DeletePeriodTransactionCommandHandler(IGenericRepository<PeriodTransaction> periodicRepo)
            {
                _periodicRepo = periodicRepo;
            }
            public async Task<ServiceRespnse> Handle(DeletePeriodTransactionCommand request, CancellationToken cancellationToken)
            {
                var exist = await _periodicRepo.GetAsNoTrackingQuery().Where(z => z.Id == request.Id).FirstOrDefaultAsync();
                if (exist is null)
                {
                    Hashtable errors = new();
                    errors.Add("Id", "Not Fpound");
                    return new ServiceRespnse().Failed(System.Net.HttpStatusCode.NotFound, errors);
                }
                await _periodicRepo.SoftDeleteAsync(exist, cancellationToken);
                return new ServiceRespnse().OK();
            }
        }
    }
}
