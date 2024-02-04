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
                if (now.DayOfWeek == _dayToSend && now.TimeOfDay > _timeToSend && now.TimeOfDay < _timeToSend.Add(TimeSpan.FromHours(1)))
                {
                    await SendWeeklyEmailsAsync();
                    // Wait until next week.
                    await Task.Delay(TimeSpan.FromDays(7), stoppingToken);
                }
                else
                {
                    // Check again in an hour.
                    await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
                }
            }
        }

        private  async Task SendWeeklyEmailsAsync()
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