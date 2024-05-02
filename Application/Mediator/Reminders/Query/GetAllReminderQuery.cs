using Application.Cqrs.Queris;
using Core.Application.Common;
using Core.Application.Mediator.Reminders.DTOs;
using Core.Domain.DTOs.Shared;
using Core.Domain.Entities.Reminders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Mediator.Reminders.Query
{
    public  class GetAllReminderQuery:IQuery<ServiceRespnse<List<ReminderDTO>>>
    {
        public int PageNumber { get; set; }
        public int Count { get; set; }

        public class GetAllReminderQueryHandler : IQueryHandler<GetAllReminderQuery, ServiceRespnse<List<ReminderDTO>>>
        {
            private readonly IGenericRepository<Reminder> _reminderRepository;

            public GetAllReminderQueryHandler(IGenericRepository<Reminder> reminderRepository)
            {
                _reminderRepository = reminderRepository;
            }

            public async Task<ServiceRespnse<List<ReminderDTO>>> Handle(GetAllReminderQuery request, CancellationToken cancellationToken)
            {
                var repo = _reminderRepository.GetAsNoTrackingQuery();
                //TODO:set filter here
                var reminder = await repo.Select(z => new ReminderDTO
                {
                    Description = z.Description,
                    TelegramId = z.TelegramId,
                    Id = z.Id,
                    IsRemindMeAgain = z.IsRemindMeAgain,
                    Mount = z.Amount,
                    RemindAgainDate = z.RemindAgainDate,
                    RemindDate = z.RemindDate
                }).Skip((request.PageNumber - 1) * request.Count).Take(request.Count).ToListAsync();
                var totalCount = await repo.CountAsync();
                return new ServiceRespnse<List<ReminderDTO>>().OK(reminder,totalCount);
            }
        }
    }
}
