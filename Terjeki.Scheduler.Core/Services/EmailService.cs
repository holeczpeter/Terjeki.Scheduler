
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Terjeki.Scheduler.Core.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _settings;
        public EmailService(IOptions<EmailSettings> opts)
        {
            _settings = opts.Value;
        }

        public async Task SendEmailAsync(string to, string subject, string htmlBody)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("WebApp", _settings.Username));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder { HtmlBody = htmlBody };
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            await client.ConnectAsync(
                _settings.SmtpServer,
                _settings.Port,
                _settings.UseSsl);
            await client.AuthenticateAsync(
                _settings.Username,
                _settings.Password);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}
