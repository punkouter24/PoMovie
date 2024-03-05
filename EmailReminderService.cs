using Microsoft.Extensions.Hosting;
using PoMovie.Services;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PoMovie.Services
{
    public class EmailReminderService : BackgroundService
    {
        private readonly TimeSpan _timeToSend = new TimeSpan(18, 0, 0); // 6 PM
        private readonly DayOfWeek _dayToSend = DayOfWeek.Saturday;
        private readonly IEmailService _emailService;

        public EmailReminderService(IEmailService emailService)
        {
            _emailService = emailService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var now = DateTime.Now;
                var nextSendTime = CalculateNextSendTime(now);

                if (now >= nextSendTime && now < nextSendTime.Add(TimeSpan.FromHours(1)))
                {
                    await SendWeeklyEmailsAsync();
                    // Calculate the delay until the send time next week.
                    nextSendTime = CalculateNextSendTime(now.AddDays(7));
                }

                var delay = nextSendTime - now;
                if (delay < TimeSpan.Zero) delay = TimeSpan.FromHours(1); // Minimum delay of 1 hour to avoid tight loop.
                await Task.Delay(delay, stoppingToken);
            }
        }

        private DateTime CalculateNextSendTime(DateTime currentTime)
        {
            // Calculate the next send time based on _dayToSend and _timeToSend.
            // This is a simplified example. You'll need to adjust it based on your requirements.
            var daysUntilSend = (_dayToSend - currentTime.DayOfWeek + 7) % 7;
            daysUntilSend = daysUntilSend == 0 ? 7 : daysUntilSend; // Ensure it's always future.
            var nextSendDate = currentTime.AddDays(daysUntilSend).Date;
            return nextSendDate.Add(_timeToSend);
        }

        private async Task SendWeeklyEmailsAsync()
        {
            // Simulated operation for sending emails
            Console.WriteLine("Sending weekly emails...");
            await _emailService.SendEmailAsync("punkouter24@gmail.com", "From PoMovie", "Don't forget to check out your movie list!");
        }

        public async Task SendWeeklyEmailsAsync(string email, string subject, string message)
        {
            // Simulated operation for sending emails
            Console.WriteLine("Sending weekly emails...");
            await _emailService.SendEmailAsync(email, subject, message);
        }
    }
}