using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;

namespace ApiBTG.Application.Common.Interfaces
{
    public class EmailNotificationService : INotificationService
    {
        private readonly EmailSettings _settings;

        public EmailNotificationService(IOptions<EmailSettings> options)
        {
            _settings = options.Value;
        }

        public async Task SendAsync(string to, string subject, string message)
        {
            var mail = new MailMessage
            {
                From = new MailAddress(_settings.SenderEmail, _settings.SenderName),
                Subject = subject,
                Body = message,
                IsBodyHtml = true
            };
            mail.To.Add(to);

            using var smtp = new SmtpClient(_settings.SmtpServer, _settings.Port)
            {
                Credentials = new NetworkCredential(_settings.Username, _settings.Password),
                EnableSsl = true
            };
            await smtp.SendMailAsync(mail);
        }
    }
} 