using Application.Cqrs.Commands;
using Core.Application.Common;
using Core.Application.Mediator.PeriodTransactions.DTOs;
using Core.Domain.DTOs.Shared;
using Core.Domain.Entities.PeriodTransactions;
using MediatR.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Core.Application.Mediator.PeriodTransactions.Commad
{
    public class InsertTransactionCommand:ICommand<ServiceRespnse>
    {
        public PeriodTransactionDTO dto { get; set; }


        public class InsertTransactionCommandHandler : ICommandHandler<InsertTransactionCommand, ServiceRespnse>
        {
            private readonly IGenericRepository<PeriodTransaction> _periodicRepo;

            public InsertTransactionCommandHandler(IGenericRepository<PeriodTransaction> periodicRepo)
            {
                _periodicRepo = periodicRepo;
            }

            public async Task<ServiceRespnse> Handle(InsertTransactionCommand request, CancellationToken cancellationToken)
            {
                await _periodicRepo.AddAsync(new PeriodTransaction
                {
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
