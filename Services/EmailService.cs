using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace PoMovie.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string subject, string htmlContent);
    }

    public class EmailService : IEmailService
    {
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string htmlContent)
        {
            try
            {
                var apiKey = _configuration["SendGridKey"];
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress("punkouter24@gmail.com", null);
                var to = new EmailAddress(toEmail);
                var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);
                var response = await client.SendEmailAsync(msg);

                if (!response.IsSuccessStatusCode)
                {
                    // Log the response body or status code for more details
                    var errorMsg = await response.Body.ReadAsStringAsync();
                    Console.WriteLine($"Email failed to send: {errorMsg}");
                }
            }
            catch (Exception ex)
            {
                // Log the exception details
                Console.WriteLine($"Exception occurred: {ex.Message}");
            }
        }

    }
}