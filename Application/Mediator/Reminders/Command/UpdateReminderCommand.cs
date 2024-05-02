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
    public class UpdateReminderCommand : ICommand<ServiceRespnse>
    {
        public ReminderDTO dto { get; set; }

        public class UpdateReminderCommandHandler : ICommandHandler<UpdateReminderCommand, ServiceRespnse>
        {
            private readonly IGenericRepository<Reminder> _reminderRepository;

            public UpdateReminderCommandHandler(IGenericRepository<Reminder> reminderRepository)
            {
                _reminderRepository = reminderRepository;
            }

            public async Task<ServiceRespnse> Handle(UpdateReminderCommand request, CancellationToken cancellationToken)
            {
                var exist = await _reminderRepository.GetAsNoTrackingQuery().AnyAsync(z => z.Id == request.dto.Id);
                if (exist)
                {
                    Hashtable errors = new();
                    errors.Add("Id","Not Fount");
                    return new ServiceRespnse().Failed(System.Net.HttpStatusCode.NotFound,errors);
                }

                await _reminderRepository.AddAsync(new Reminder
                {
                    TelegramId = request.dto.TelegramId,
                    Id = request.dto.Id,
                    Amount = request.dto.Mount,
                    Description = request.dto.Description,
                    IsRemindMeAgain = request.dto.IsRemindMeAgain,
                    RemindDate = request.dto.RemindDate,
                    RemindAgainDate = request.dto.RemindAgainDate,
                });
                return new ServiceRespnse().OK();
            }
        }
    }
}
