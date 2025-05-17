using System.Net.Mail;
using System.Net;
using Microsoft.Extensions.Options;
using EmailService.Worker.Settings;

namespace EmailService.Worker.Services
{
    public class EmailAppService(IOptions<EmailSettings> _options) : IEmailService
    {
        private readonly EmailSettings _settings = _options.Value;

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                var smtpClient = new SmtpClient(_settings.SmtpHost)
                {
                    Port = _settings.SmtpPort,
                    Credentials = new NetworkCredential(_settings.Username, _settings.Password),
                    EnableSsl = true,
                };

                var mailMessage = new MailMessage
                {
                    From = new MailAddress(_settings.From),
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = false,
                };

                mailMessage.To.Add(to);
                await smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {
                // log, throw err
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
        }

    }
}
