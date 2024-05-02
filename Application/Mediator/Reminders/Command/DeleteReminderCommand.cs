using Application.Cqrs.Commands;
using Core.Application.Common;
using Core.Application.Mediator.Reminders.DTOs;
using Core.Domain.DTOs.Shared;
using Core.Domain.Entities.Reminders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Core.Application.Mediator.Reminders.Command
{
    public class DeleteReminderCommand:ICommand<ServiceRespnse>
    {
        public long Id { get; set; }

        public class DeleteReminderCommandHandler : ICommandHandler<DeleteReminderCommand, ServiceRespnse>
        {
            private readonly IGenericRepository<Reminder> _reminderRepository;

            public DeleteReminderCommandHandler(IGenericRepository<Reminder> reminderRepository)
            {
                _reminderRepository = reminderRepository;
            }

            public async Task<ServiceRespnse> Handle(DeleteReminderCommand request, CancellationToken cancellationToken)
            {
                var reminder = await _reminderRepository.GetAsNoTrackingQuery().Where(z => z.Id == request.Id).FirstOrDefaultAsync();
                if (reminder is null)
                {
                    Hashtable errors = new();
                    errors.Add("Id", "Not Fount");
                    return new ServiceRespnse().Failed(System.Net.HttpStatusCode.NotFound, errors);
                }
                await _reminderRepository.SoftDeleteAsync(reminder, cancellationToken);
                return new ServiceRespnse().OK();
            }
        }
    }
}
