using Application.Cqrs.Commands;
using Core.Application.Common;
using Core.Application.Mediator.Reminders.Command;
using Core.Domain.DTOs.Shared;
using Core.Domain.Entities.PeriodTransactions;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Core.Application.Mediator.PeriodTransactions.Commad
{
    public class SendPeriodRemindTelegramMessageCommand:ICommand<ServiceRespnse>
    {
        public class SendPeriodRemindTelegramMessageCommandHandler : ICommandHandler<SendTelegramReminderCommand, ServiceRespnse>
        {
            private readonly IGenericRepository<PeriodTransaction> _periodTransactionRepository;

            public SendPeriodRemindTelegramMessageCommandHandler(IGenericRepository<PeriodTransaction> periodTransactionRepository)
            {
                _periodTransactionRepository = periodTransactionRepository;
            }
            public async Task<ServiceRespnse> Handle(SendTelegramReminderCommand request, CancellationToken cancellationToken)
            {

                var reminders = await _periodTransactionRepository.GetAsNoTrackingQuery().Where(z => z.PayDate.Date == DateTime.Now.Date.AddDays(-1)).ToListAsync();

                //telegram config
                var ApiKey = "7103099486:AAGyvP7tji0wMat1NqlDgTJitlsavFtzcGg";
                reminders.ForEach(z =>
                {
                    TelegramSendMessage(ApiKey, z.TelegramId.ToString(), $"با سلام جهت یادآوری به قیمت {z.Amount} بابت {z.Description}");
                });
                return new ServiceRespnse().OK();

            }

            public async void TelegramSendMessage(string apilToken, string destID, string text)
            {
                string urlString = $"https://api.telegram.org/bot{apilToken}/sendMessage?chat_id={destID}&text={text}";

                WebClient webclient = new WebClient();

                webclient.DownloadStringAsync(new Uri(urlString));
            }
        }
    }
}
