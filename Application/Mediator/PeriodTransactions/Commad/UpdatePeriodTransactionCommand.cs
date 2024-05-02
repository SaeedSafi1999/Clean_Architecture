using Application.Cqrs.Commands;
using Core.Application.Common;
using Core.Application.Mediator.PeriodTransactions.DTOs;
using Core.Domain.DTOs.Shared;
using Core.Domain.Entities.PeriodTransactions;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Windows.Input;

namespace Core.Application.Mediator.PeriodTransactions.Commad
{
    public class UpdatePeriodTransactionCommand : ICommand<ServiceRespnse>
    {
        public PeriodTransactionDTO dto { get; set; }


        public class UpdatePeriodTransactionCommandHadnler : ICommandHandler<UpdatePeriodTransactionCommand, ServiceRespnse>
        {
            private readonly IGenericRepository<PeriodTransaction> _periodicRepo;

            public UpdatePeriodTransactionCommandHadnler(IGenericRepository<PeriodTransaction> periodicRepo)
            {
                _periodicRepo = periodicRepo;
            }

            public async Task<ServiceRespnse> Handle(UpdatePeriodTransactionCommand request, CancellationToken cancellationToken)
            {
                var exist = await _periodicRepo.GetAsNoTrackingQuery().AnyAsync(z => z.Id == request.dto.Id);
                if (!exist)
                {
                    Hashtable errors = new();
                    errors.Add("Id", "Not Fpound");
                    return new ServiceRespnse().Failed(System.Net.HttpStatusCode.NotFound,errors);
                }

                await _periodicRepo.AddAsync(new PeriodTransaction
                {
                    Id = request.dto.Id,
                    Amount = request.dto.Amount,
                    TotalAmount = request.dto.TotalAmount,
                    Description = request.dto.Description,
                    PayDate = request.dto.PayDate,
                    RemindMe = request.dto.RemindMe,
                });
                return new ServiceRespnse().OK();
            }
        }
    }
}
